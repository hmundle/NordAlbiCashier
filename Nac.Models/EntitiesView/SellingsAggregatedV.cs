using Nac.Models.Entities;

namespace Nac.Models.EntitiesView;

[Keyless]
public partial class SellingsAggregatedV
{
    [DisplayName("Produkt")]
    public Guid? ProductId { get; set; }

    [DisplayName("Kategorie")]
    public ProductCategory Category { get; set; } = ProductCategory.Code;

    [DisplayName("Gruppe")]
    public ProductGroup? Group { get; set; } = null;

    [DisplayName("Bar Code")]
    public string BarCode { get; set; } = string.Empty;

    [DisplayName("Beschreibung")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Grundpreis")]
    public double SinglePrice { get; set; } = 0.0;

    [DisplayName("Grundsonderpreis")]
    public double? SinglePriceReduced { get; set; } = null;

    [DisplayName("Gesamtanzahl")]
    public int SumQuantity { get; set; } = 1;

    [DisplayName("Gesamt-Preiseingabe")]
    public double SumPriceManual { get; set; } = 0.0;

    [DisplayName("Gesamtgewicht")]
    public double SumWeight { get; set; } = 0.0;

    [DisplayName("Gesamt-Endpreis")]
    public double SumFinalPrice { get; set; } = 0.0;

    [DisplayName("Anzahl Einkäufe")]
    public int Count { get; set; } = 0;
}
