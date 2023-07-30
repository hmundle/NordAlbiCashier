using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Mvc.Controllers.Base;

namespace Nac.Mvc.Controllers;

public class SellingsController : BaseCrudController<Selling, SellingsController>
{

    public SellingsController(IAppLogging<SellingsController> appLogging, ISellingRepo mainRepo, IUserRepo userRepo) 
    : base(appLogging, mainRepo, userRepo)
    {
    }

}
