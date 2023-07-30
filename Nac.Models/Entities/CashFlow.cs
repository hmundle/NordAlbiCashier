namespace Nac.Models.Entities;

[Index(nameof(Created), IsUnique = false)]
public partial class CashFlow : CashBase
{
}
