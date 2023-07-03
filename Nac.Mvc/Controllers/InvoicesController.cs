using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Mvc.Controllers.Base;

namespace Nac.Mvc.Controllers;

public class InvoicesController : BaseCrudController<Invoice, InvoicesController>
{


    public InvoicesController(IAppLogging<InvoicesController> appLogging, IInvoiceRepo mainRepo) 
    : base(appLogging, mainRepo)
    {
    }


}