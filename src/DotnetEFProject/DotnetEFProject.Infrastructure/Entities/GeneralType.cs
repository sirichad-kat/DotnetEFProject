namespace DotnetEFProject.Infrastructure.Entities;

public partial class GeneralType
{
    public string Gdtype { get; set; } = null!;

    public string? NameLocal { get; set; }

    public string? NameEng { get; set; }

    public char? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyUser { get; set; }

    public virtual ICollection<GeneralDesc> GeneralDescs { get; set; } = new List<GeneralDesc>();
}
