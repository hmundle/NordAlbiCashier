using Microsoft.EntityFrameworkCore;
using Nac.Dal.EfStructures;
using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Models.EntitiesView;
using Nac.Mvc.Controllers.Base;
using Nac.Mvc.Utilities;

namespace Nac.Mvc.Controllers;

public class SellingsController : BaseCrudController<Selling, SellingsController>
{
    private readonly NacDbContext _context;

    public SellingsController(IAppLogging<SellingsController> appLogging, ISellingRepo mainRepo, IUserRepo userRepo, NacDbContext context) 
    : base(appLogging, mainRepo, userRepo)
    {
        _context = context;
    }

    public class SellingSearchModel
    {
        public IList<ProductCategory> CategoryOrFilter { get; set; } = null!;
        public IList<ProductGroup> GroupOrFilter { get; set; } = null!;
    }

    private static IQueryable<SellingsAggregatedV> AddFiltersToQuery(SellingSearchModel searchModel, IQueryable<SellingsAggregatedV> query)
    {
        if (searchModel.CategoryOrFilter.Count > 0)
        {
            query = query.Where(x => searchModel.CategoryOrFilter.Contains(x.Category));
        }
        if (searchModel.GroupOrFilter.Count > 0)
        {
            query = query.Where(x => x.Group != null && searchModel.GroupOrFilter.Contains(x.Group.Value));
        }
        return query;
    }

    private static IQueryable<SellingsAggregatedV> AddConditionsToQuery(
        IEnumerable<ControllerHelper.DataTablesSearchBuilderCondition> conditions, IQueryable<SellingsAggregatedV> query)
    {
        foreach (var condition in conditions)
        {
            switch (condition.OrigData)
            {
                default:
                    break;
                case "barCode":
                    query = ControllerHelper.QueryForStringProperty(condition, query, "BarCode");
                    break;
                case "name":
                    query = ControllerHelper.QueryForStringProperty(condition, query, "Name");
                    break;
            }
        }
        return query;
    }

    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public override async Task<IActionResult> IndexAsync() => await Task.FromResult(View());

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> GetListAsync()
    {
        var dataTablesRequest = ControllerHelper.ExtractDataTablesRequest(Request);

        var query = _context.SellingsAggregatedV.AsQueryable();

        // search pane request
        SellingSearchModel searchModel = new()
        {
            CategoryOrFilter = ControllerHelper.ExtractFilterEnum<ProductCategory>(Request, "category"),
            GroupOrFilter = ControllerHelper.ExtractFilterEnum<ProductGroup>(Request, "group"),
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
        var matList = await query.Skip(dataTablesRequest.Skip).Take(dataTablesRequest.PageSize).ToListAsync();

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
                    category = ControllerHelper.ProductCategoryValues,
                    group = ControllerHelper.ProductGroupValues,
                }
            }
        };
        return Ok(returnObj);
    }

}
