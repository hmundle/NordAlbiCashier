using Nac.Models.Entities;
using Nac.Models.Utilities;

namespace Nac.Models.EntitiesView;

[Keyless]
public partial class SellingsAggregatedV
{
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

    [DisplayName("Datum")]
    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? NewestCreated { get; set; }

    [DisplayName("Jüngste Änderung")]
    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? NewestModified { get; set; }

    [DisplayName("Kasse")]
    public string? Operator { get; set; } = "unknown";
}
