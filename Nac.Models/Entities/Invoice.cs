namespace Nac.Models.Entities;

[Index(nameof(Created), IsUnique = false)]
public partial class Invoice : BaseEntity
{
    // reference to Sellings
    [JsonIgnore]
    public IEnumerable<Selling> Sellings { get; set; } = new List<Selling>();

    [DisplayName("Bezahlart")]
    [Required]
    public PaymentType Type { get; set; } = PaymentType.Cash;
}
