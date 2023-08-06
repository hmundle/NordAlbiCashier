using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Mvc.Controllers.Base;

namespace Nac.Mvc.Controllers;

public class CashStatusController : BaseCrudController<CashStatus, CashStatusController>
{

    public CashStatusController(IAppLogging<CashStatusController> appLogging, ICashStatusRepo mainRepo, IUserRepo userRepo)
    : base(appLogging, mainRepo, userRepo)
    {
    }

    [NonAction]
    public override Task<IActionResult> IndexAsync() => base.IndexAsync();

    [HttpGet]
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public virtual async Task<IActionResult> IndexAsync(string? till)
    {
        var operatorList = new List<string>
        {
            "Alle"
        };
        if (till == operatorList[0])
        {
            till = null;
        }
        operatorList.AddRange(await UserRepo.GetAllUsers().Select(u => u.Name).ToListAsync());
        ViewData["AllUsers"] = new SelectList(operatorList, till ?? operatorList[0]);
        var query = MainRepo.GetAll();
        if (till != null)
        {
            query = query.Where(cs => cs.Till == till);
        }
        query = query.OrderBy(cs => cs.Created).Reverse();
        return View(await query.ToListAsync());
    }

}
