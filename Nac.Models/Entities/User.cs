namespace Nac.Models.Entities;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Created), IsUnique = false)]
public partial class User : BaseEntity
{
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Name { get; set; } = "";
}
