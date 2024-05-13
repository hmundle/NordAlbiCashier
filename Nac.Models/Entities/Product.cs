namespace Nac.Models.Entities;

[Index(nameof(Created), IsUnique = false)]
[Index(nameof(BarCode), IsUnique = true)]
public partial class Product : BaseEntity
{
    // reference to Sellings
    [JsonIgnore]
    public IEnumerable<Selling> Sellings { get; set; } = new List<Selling>();

    [DisplayName("Kategorie")]
    [Required]
    public ProductCategory Category { get; set; } = ProductCategory.Code;

    [DisplayName("Bar Code")]
    [Required]
    [MinLength(1)]
    public String BarCode { get; set; } = string.Empty;

    [DisplayName("Beschreibung")]
    [Required]
    public String Name { get; set; } = string.Empty;

    [DisplayName("Preis")]
    [Required]
    public double Price { get; set; } = 0.0;

    [DisplayName("Sonderpreis")]
    public double? PriceReduced { get; set; } = null;

    [DisplayName("Kommentar")]
    public String? Comment { get; set; } = null;

    [DisplayName("Geliefert")]
    public int Delivered { get; set; } = 0;

    [DisplayName("Gruppe")]
    public ProductGroup? Group { get; set; } = null;

    [Required]
    [DisplayName("Aktiv")]
    public bool IsActive { get; set; } = false;
}
