using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Mvc.Controllers.Base;
using Nac.Mvc.Models;

namespace Nac.Mvc.Controllers;

public class InvoicesController : BaseCrudController<Invoice, InvoicesController>
{
    public InvoicesController(IAppLogging<InvoicesController> appLogging, IInvoiceRepo mainRepo, IUserRepo userRepo)
    : base(appLogging, mainRepo, userRepo)
    {
    }

    [HttpGet]
    public async Task<IActionResult> NewAsync()
    {
        var invoice = new Invoice()
        {
            Type = PaymentType.Pending,
            Operator = User.Identity?.Name ?? "unknown"
    };
        await MainRepo.AddAsync(invoice);
        // switch to next payment type
        invoice.Type = PaymentType.Cash;
        return View(invoice);
    }


    [HttpPost("{id?}")]
    [Produces("application/json")]
    public async Task<IActionResult> AddSellingsAsync(Guid? id, [FromBody] Pay pay, [FromServices] ISellingRepo sellingRepo)
    {
        if (id == null || pay == null)
        {
            return BadRequest();
        }
        var entity = await MainRepo.FindAsync(id);
        if (entity == null)
        {
            return BadRequest();
        }
        entity.Type = pay.Type;
        // set the modified data
        entity.Modified = DateTime.UtcNow;
        entity.Operator = User.Identity?.Name ?? "unknown";
        foreach (var s in pay.Sellings)
        {
            s.Operator = entity.Operator;
            s.Created = entity.Modified;
            s.Modified = null;
        }
        await MainRepo.UpdateAsync(entity);

        await sellingRepo.AddRangeAsync(pay.Sellings);

        return Ok(pay.Sellings.Count());
    }
}
