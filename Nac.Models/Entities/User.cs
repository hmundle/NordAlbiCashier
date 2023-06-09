namespace Nac.Models.Entities;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Created), IsUnique = false)]
public partial class User : BaseEntity
{
    [Required, StringLength(50)]
    public string Name { get; set; } = "";
}
