using Microsoft.EntityFrameworkCore;
using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Mvc.Controllers.Base;
using Nac.Mvc.Models;
using Nac.Mvc.Utilities;
using System.Linq;
using static Nac.Mvc.Utilities.ControllerHelper;

namespace Nac.Mvc.Controllers;

public class InvoicesController : BaseCrudController<Invoice, InvoicesController>
{
    public InvoicesController(IAppLogging<InvoicesController> appLogging, IInvoiceRepo mainRepo, IUserRepo userRepo)
    : base(appLogging, mainRepo, userRepo)
    {
    }

    public class InvoiceSearchModel
    {
        public IList<PaymentType> PaymentOrFilter { get; set; }
        public IList<string> OperatorOrFilter { get; set; }
    }

    private static IQueryable<Invoice> AddFiltersToQuery(InvoiceSearchModel searchModel, IQueryable<Invoice> query)
    {
        if (searchModel.PaymentOrFilter.Count > 0)
        {
            query = query.Where(x => searchModel.PaymentOrFilter.Contains(x.Type));
        }
        if (searchModel.OperatorOrFilter.Count > 0)
        {
            query = query.Where(x => searchModel.OperatorOrFilter.Contains(x.Operator));
        }
        return query;
    }

    private static IQueryable<Invoice> AddConditionsToQuery(
        IEnumerable<ControllerHelper.DataTablesSearchBuilderCondition> conditions, IQueryable<Invoice> query)
    {
        foreach (var condition in conditions)
        {
            switch (condition.OrigData)
            {
                default:
                    break;
                case "comment":
                    query = ControllerHelper.QueryForStringProperty(condition, query, "Comment");
                    break;
                case "created":
                    query = ControllerHelper.QueryForDateTimeProperty(condition, query, "Created");
                    break;
            }
        }
        return query;
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> GetListAsync()
    {
        var dataTablesRequest = ControllerHelper.ExtractDataTablesRequest(Request);

        var query = MainRepo.GetAll();

        // search pane request
        InvoiceSearchModel searchModel = new()
        {
            PaymentOrFilter = ControllerHelper.ExtractFilterEnum<PaymentType>(Request, "type"),
            OperatorOrFilter = ControllerHelper.ExtractFilterString(Request, "operator"),
        };

        // search builder request
        var listConditions = ControllerHelper.ExtractSearchBuilderConditions(Request);

        // get total count of data in table
        dataTablesRequest.TotalRecord = await query.CountAsync();

        query = AddFiltersToQuery(searchModel, query);
        query = AddConditionsToQuery(listConditions, query);

        // get total count of records after search
        dataTablesRequest.FilterRecord = await query.CountAsync();

        // sort data
        if (!string.IsNullOrEmpty(dataTablesRequest.SortColumn))
        {
            query = query.OrderBy(x => EF.Property<object>(x, dataTablesRequest.SortColumn));
            if (!dataTablesRequest.SortColumnDirectionIsAsc)
            {
                query = query.Reverse();
            }
        }
        // pagination
        query = query.Skip(dataTablesRequest.Skip).Take(dataTablesRequest.PageSize);
        var matList = await query.Select(i => new
        {
            i.Id,
            i.Type,
            i.Comment,
            i.Operator,
            i.Created,
            i.Modified,
            Count = i.Sellings.Count(),
            Sum = i.Sellings.Sum(s => s.FinalPrice)
        })
        .ToListAsync();

        var returnObj = new
        {
            draw = dataTablesRequest.Draw,
            recordsTotal = dataTablesRequest.TotalRecord,
            recordsFiltered = dataTablesRequest.FilterRecord,
            data = matList,
            searchPanes = new
            {
                options = new
                {
                    type = ControllerHelper.PaymentTypeValues,
                    @operator = UserRepo.GetAllUsers().Select(u => new DataTableSearchPaneContent { Label = u.Name, Value = u.Name }).ToList(),
                }
            }
        };
        return Ok(returnObj);
    }

    [HttpGet]
    public async Task<IActionResult> NewAsync()
    {
        return await Task.FromResult(View());
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> AddSellingsAsync([FromBody] Pay pay, [FromServices] ISellingRepo sellingRepo)
    {
        if (pay == null || pay.Invoice == null)
        {
            return BadRequest();
        }
        pay.Invoice.Operator = User.Identity?.Name ?? "unknown";

        foreach (var s in pay.Sellings)
        {
            s.Operator = pay.Invoice.Operator;
            // clean up some content, which is supposed to be set freshly by the DB
            // this was set due to the characteristics of the datatables.net, which
            // data is send to this API. It contains values from the product table, with 
            // the same column names.
            s.Created = null;
            s.Modified = null;
            s.xmin = 0;
        }
        pay.Invoice.Sellings = pay.Sellings;

        await MainRepo.AddAsync(pay.Invoice);

        return Ok(pay.Sellings.Count());
    }
}
