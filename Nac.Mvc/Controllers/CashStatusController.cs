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

}
