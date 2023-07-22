using Nac.Models.Entities;
using System.ComponentModel;

namespace Nac.Mvc.Models;

public class Pay
{
    public Guid Id { get; set; }

    public IEnumerable<Selling> Sellings { get; set; } = new List<Selling>();

    public PaymentType Type { get; set; } = PaymentType.Cash;
}
