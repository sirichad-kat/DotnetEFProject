namespace DotnetEFProject.Infrastructure.Entities;

public partial class GeneralDesc
{
    public int Id { get; set; }

    public string Gdtype { get; set; } = null!;

    public string Gdcode { get; set; } = null!;

    public string? Desc1 { get; set; }

    public string? Desc2 { get; set; }

    public string? Desc3 { get; set; }

    public string? Desc4 { get; set; }

    public string? Desc5 { get; set; }

    public string? Cond1 { get; set; }

    public string? Cond2 { get; set; }

    public string? Cond3 { get; set; }

    public string? Cond4 { get; set; }

    public string? Cond5 { get; set; }

    public char? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyUser { get; set; }

    public virtual GeneralType GdtypeNavigation { get; set; } = null!;
}
