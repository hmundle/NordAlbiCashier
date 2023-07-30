namespace Nac.Models.Entities;

[Index(nameof(Created), IsUnique = false)]
public partial class CashStatus : BaseEntity
{
    // columns
    [DisplayName("Kasse")]
    [Required]
    public string Till { get; set; } = string.Empty;

    [DisplayName("Type")]
    [Required]
    public string Type { get; set; } = string.Empty;

    [DisplayName("Grund")]
    [Required]
    public string Description { get; set; } = string.Empty;

    [DisplayName("500€")]
    [Required]
    public double _500 { get; set; } = 0.0;

    [DisplayName("200€")]
    [Required]
    public double _200 { get; set; } = 0.0;

    [DisplayName("100€")]
    [Required]
    public double _100 { get; set; } = 0.0;

    [DisplayName("50€")]
    [Required]
    public double _50 { get; set; } = 0.0;

    [DisplayName("20€")]
    [Required]
    public double _20 { get; set; } = 0.0;

    [DisplayName("10€")]
    [Required]
    public double _10 { get; set; } = 0.0;

    [DisplayName("5€")]
    [Required]
    public double _5 { get; set; } = 0.0;

    [DisplayName("2€")]
    [Required]
    public double _2 { get; set; } = 0.0;

    [DisplayName("1€")]
    [Required]
    public double _1 { get; set; } = 0.0;

    [DisplayName("0,50€")]
    [Required]
    public double _050 { get; set; } = 0.0;

    [DisplayName("0,20€")]
    [Required]
    public double _020 { get; set; } = 0.0;

    [DisplayName("0,10€")]
    [Required]
    public double _010 { get; set; } = 0.0;

    [DisplayName("0,05€")]
    [Required]
    public double _005 { get; set; } = 0.0;

    [DisplayName("0,02€")]
    [Required]
    public double _002 { get; set; } = 0.0;

    [DisplayName("0,01€")]
    [Required]
    public double _001 { get; set; } = 0.0;

}
