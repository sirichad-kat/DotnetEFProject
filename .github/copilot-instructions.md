### Custom Mapping Pattern (Read Side: Entity → DTO)
When I ask to "map this" or "generate mapper", follow these rules:
1. **Technique**: Manual Mapping using C# Extension Methods (No 3rd-party libraries).
2. **Null Safety**: Always include null checks for source objects.
3. **Naming Convention**:
   - Single object: `public static [Target] ToDto(this [Source] source)`
   - Collection: `public static List<[Target]> ToDtoList(this IEnumerable<[Source]> source)`
4. **Nested Objects**: Use the defined `ToDto()` and `ToDtoList()` for all nested properties to maintain the pattern.
5. **Efficiency**: Only map properties that exist in the DTO (Projection style).
6. **IQueryable Projection**: Always generate an additional `Expression<Func<[Source], [Target]>>` property or method for use with IQueryable (e.g., EF Core `.Select()`):
   - **Naming Convention**: `public static Expression<Func<[Source], [Target]>> ToExpression()`
   - **Purpose**: Enables server-side projection — the mapping is translated to SQL and only selected columns are fetched from the database.
   - **Constraint**: Must use only initializer syntax (`new [Target] { ... }`) inside the expression body. Do **not** call instance methods or `ToDto()` inside the expression (not translatable by EF Core).
   - **Nested Objects**: Inline the nested projection using `new [NestedTarget] { ... }` syntax directly. Do not reference other `Expression<>` variables inside (not composable at the expression-tree level without explicit expansion).
   - **Collections**: Use `.Select(x => new [NestedTarget] { ... }).ToList()` inline within the expression for navigation collection properties.
   - **Usage Pattern**:
     ```csharp
     // In-memory mapping
     var dto = entity.ToDto();

     // IQueryable projection (DB side)
     var dtos = dbContext.Entities
         .Where(x => x.IsActive)
         .Select([SourceMapper].ToExpression())
         .ToListAsync();
     ```
   - **Example output**:
     ```csharp
     public static class ProductMapper
     {
         // In-memory mapping
         public static ProductDto ToDto(this Product source)
         {
             if (source is null) return null;
             return new ProductDto
             {
                 Id       = source.Id,
                 Name     = source.Name,
                 Price    = source.Price,
                 Category = source.Category?.ToDto()
             };
         }

         public static List<ProductDto> ToDtoList(this IEnumerable<Product> source)
             => source?.Select(x => x.ToDto()).ToList() ?? [];

         // IQueryable projection
         public static Expression<Func<Product, ProductDto>> ToExpression()
             => source => new ProductDto
             {
                 Id    = source.Id,
                 Name  = source.Name,
                 Price = source.Price,
                 Category = source.Category == null ? null : new CategoryDto
                 {
                     Id   = source.Category.Id,
                     Name = source.Category.Name
                 }
             };
     }
     ``` 

### Write Mapper Pattern (Write Side: Input/Command → Entity)
When I ask to "map input to entity", "generate entity mapper", or "map command to entity", follow these rules:

1. **Technique**: Manual Mapping using C# Extension Methods (No 3rd-party libraries).
2. **Null Safety**: Always include null checks for source objects. Return `null!` for single object or `[]` for collections.
3. **File Location**: Place in `[Project].Infrastructure/Mappings/[Feature]/[Feature]Mapper.cs`
4. **Naming Convention**:
   - Parent entity: `public static [TargetEntity] To[TargetEntity]Entity(this [SourceInput] source)`
   - Child entity list (projected from parent input): `public static List<[ChildEntity]> To[ChildEntity]EntityList(this [SourceInput] source)`
5. **DateTime Handling**: Always wrap `DateTime` with `DateTime.SpecifyKind(..., DateTimeKind.Unspecified)` when mapping from nullable or input DateTime values to avoid PostgreSQL/EF Core timezone issues.
6. **Child Collection Pattern**: When the parent input contains a child list (e.g., `source.ItemList`), generate a **separate method** on the parent input that projects the child list — passing any shared parent-level fields (e.g., `Gdtype`, `HeaderId`) into each child row explicitly.
7. **No Navigation Property assignment**: Do NOT assign `.Children = ...` inside the parent mapper method. Child lists are saved separately by the handler/repository.
8. **Example output**:
   ```csharp
   public static class GdcodeMapper
   {
       // Parent: Input → Parent Entity
       public static GeneralType ToGeneralTypeEntity(this AddGdcodeInput source)
       {
           if (source is null) return null!;
           return new GeneralType
           {
               Gdtype     = source.Gdtype,
               NameLocal  = source.NameLocal,
               NameEng    = source.NameEng,
               Status     = source.Status,
               CreateUser = source.CreateUser,
               CreateDate = source.CreateDate.HasValue
                            ? DateTime.SpecifyKind(source.CreateDate.Value, DateTimeKind.Unspecified)
                            : null
           };
       }

       // Child list: projected from parent input, inheriting parent-level key
       public static List<GeneralDesc> ToGeneralDescEntityList(this AddGdcodeInput source)
       {
           if (source?.GdList is null) return [];
           return source.GdList.Select(row => new GeneralDesc
           {
               Gdcode     = row.Gdcode,
               Gdtype     = source.Gdtype,   // <-- inherited from parent
               Desc1      = row.Desc1,
               Desc2      = row.Desc2,
               Cond1      = row.Cond1,
               Cond2      = row.Cond2,
               Status     = row.Status,
               CreateUser = row.CreateUser,
               CreateDate = row.CreateDate.HasValue
                            ? DateTime.SpecifyKind(row.CreateDate.Value, DateTimeKind.Unspecified)
                            : null
           }).ToList();
       }
   }
   ```
9. **Usage in Handler**:
   ```csharp
   var header  = input.ToGeneralTypeEntity();
   var details = input.ToGeneralDescEntityList();

   await _unitOfWork.GeneralTypes.AddAsync(header, cancellationToken);
   await _unitOfWork.GeneralDescs.AddRangeAsync(details, cancellationToken);
   await _unitOfWork.SaveChangesAsync(cancellationToken);
   ```
10. **How to prompt**:
    ```
    map input to entity สำหรับ [Feature]
    Input คือ [InputClass] มี child list ชื่อ [ChildListProperty]
    Parent Entity คือ [ParentEntity]
    Child Entity คือ [ChildEntity]
    shared key จาก parent ที่ต้องส่งไป child คือ [fieldName]
    ```

### EF Core Entity Configuration Separation Pattern
When I ask to "extract configuration", "split DbContext", or "separate OnModelCreating", follow these rules:

1. **Technique**: Use `IEntityTypeConfiguration<TEntity>` — one file per entity.
2. **File Location**: Place each configuration file in:
   `[Project].Infrastructure/Persistence/Configurations/[EntityName]Configuration.cs`
3. **Naming Convention**: `[EntityName]Configuration`
4. **Class Structure**:
   - Implement `IEntityTypeConfiguration<TEntity>`
   - Accept optional `string? schemaName` via constructor for schema flexibility
   - Move all fluent API calls from `OnModelCreating` into the `Configure()` method as-is
   ```csharp
   public class [Entity]Configuration : IEntityTypeConfiguration<[Entity]>
   {
       private readonly string? _schemaName;

       public [Entity]Configuration(string? schemaName = null)
       {
           _schemaName = schemaName;
       }

       public void Configure(EntityTypeBuilder<[Entity]> builder)
       {
           // Move all entity.Xxx(...) calls here, replacing "entity" with "builder"
       }
   }
   ```
5. **DbContext Cleanup**: After extracting, replace the `modelBuilder.Entity<T>(entity => { ... })` block in `OnModelCreating` with:
   ```csharp
   modelBuilder.ApplyConfiguration(new [Entity]Configuration(_schemaName));
   ```
6. **Schema Handling**: If the DbContext has a fixed schema (e.g., `"dotnet"`), pass it as a constructor parameter or define `_schemaName` at the DbContext level and forward it to each configuration.
7. **Example**:
   - Before (in `ApplicationDbContext.cs`):
     ```csharp
     modelBuilder.Entity<AxonsCollab>(entity =>
     {
         entity.HasKey(e => e.Id).HasName("axons_collab_pkey");
         entity.ToTable("axons_collab", "dotnet");
         entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
         // ...
     });
     ```
   - After (`AxonsCollabConfiguration.cs`):
     ```csharp
     public class AxonsCollabConfiguration : IEntityTypeConfiguration<AxonsCollab>
     {
         private readonly string? _schemaName;
         public AxonsCollabConfiguration(string? schemaName = null) => _schemaName = schemaName;

         public void Configure(EntityTypeBuilder<AxonsCollab> builder)
         {
             builder.HasKey(e => e.Id).HasName("axons_collab_pkey");
             builder.ToTable("axons_collab", _schemaName);
             builder.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
             // ...
         }
     }
     ```
   - In `ApplicationDbContext.cs`:
     ```csharp
     modelBuilder.ApplyConfiguration(new AxonsCollabConfiguration("dotnet"));
     ```
