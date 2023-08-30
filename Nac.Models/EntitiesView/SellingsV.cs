using Nac.Models.Entities;
using Nac.Models.Utilities;

namespace Nac.Models.EntitiesView;

[Keyless]
public partial class SellingsV
{
    [DisplayName("ID")]
    public Guid? SellingId { get; set; }

    [DisplayName("Produkt")]
    public Guid? ProductId { get; set; }

    [DisplayName("Bar Code")]
    public string BarCode { get; set; } = string.Empty;

    [DisplayName("Beschreibung")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Kategorie")]
    public ProductCategory Category { get; set; } = ProductCategory.Code;

    [DisplayName("Gruppe")]
    public ProductGroup? Group { get; set; } = null;

    [DisplayName("Grundpreis")]
    public double BasePrice { get; set; } = 0.0;

    [DisplayName("Grundsonderpreis")]
    public double? BasePriceReduced { get; set; } = null;

    [DisplayName("Anzahl")]
    public int Quantity { get; set; } = 1;

    [DisplayName("Preiseingabe")]
    public double PriceManual { get; set; } = 0.0;

    [DisplayName("Gewicht")]
    public double Weight { get; set; } = 0.0;

    [DisplayName("Endpreis")]
    public double FinalPrice { get; set; } = 0.0;

    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? SellingCreated { get; set; }

    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? SellingModified { get; set; }
}
