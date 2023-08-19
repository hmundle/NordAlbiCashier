using Nac.Models.Entities;
using System.ComponentModel;

namespace Nac.Mvc.Models;

public class Pay
{
    public Invoice? Invoice { get; set; }

    public IEnumerable<Selling> Sellings { get; set; } = new List<Selling>();
}
