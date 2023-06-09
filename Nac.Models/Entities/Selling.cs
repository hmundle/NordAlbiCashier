namespace Nac.Models.Entities;

[Index(nameof(Created), IsUnique = false)]
public partial class Selling : BaseEntity
{
    // foreign key to product
    // see fluent API
    [DisplayName("Produkt")]
    public Guid? ProductId { get; set; }
    // for whatever reason there is a ForeignKey needed here, to avoid strange FK name
    [ForeignKey(nameof(ProductId))]
    public Product? ProductNavigation { get; set; }

    [DisplayName("Rechnung")]
    public Guid? InvoiceId { get; set; }
    // for whatever reason there is a ForeignKey needed here, to avoid strange FK name
    [ForeignKey(nameof(InvoiceId))]
    public Invoice? InvoiceNavigation { get; set; }


    // columns
    [DisplayName("Anzahl")]
    [Required]
    public int Quantity { get; set; } = 1;

    [DisplayName("Preiseingabe")]
    [Required]
    public double PriceManual { get; set; } = 0.0;

    [DisplayName("Gewicht")]
    [Required]
    public double Weight { get; set; } = 0.0;

    [DisplayName("Endpreis")]
    [Required]
    public double FinalPrice { get; set; } = 0.0;

}
