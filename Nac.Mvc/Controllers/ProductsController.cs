using Microsoft.EntityFrameworkCore;
using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Mvc.Controllers.Base;
using Nac.Mvc.Utilities;

namespace Nac.Mvc.Controllers;

public class ProductsController : BaseCrudController<Product, ProductsController>
{
    //private readonly IMakeDataService _lookupDataService;

    public ProductsController(IAppLogging<ProductsController> appLogging, IProductRepo mainRepo) 
    : base(appLogging, mainRepo)
    {
    }

    public class ProductSearchModel
    {
        public List<ProductCategory> CategoryOrFilter { get; set; } = new();
    }

    private static IQueryable<Product> AddFiltersToQuery(ProductSearchModel searchModel, IQueryable<Product> query)
    {
        if (searchModel.CategoryOrFilter.Count > 0)
        {
            query = query.Where(x => searchModel.CategoryOrFilter.Contains(x.Category));
        }
        return query;
    }

    private static IQueryable<Product> AddConditionsToQuery(
        List<ControllerHelper.DataTablesSearchBuilderCondition> conditions, IQueryable<Product> query)
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

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> GetListAsync()
    {
        var dataTablesRequest = ControllerHelper.ExtractDataTablesRequest(Request);

        var query = MainRepo.GetAll();

        // search pane request
        ProductSearchModel searchModel = new()
        {
            CategoryOrFilter = ControllerHelper.ExtractFilterEnum<ProductCategory>(Request, "category"),
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
                }
            }
        };
        return Ok(returnObj);
    }

    [HttpGet("{barCode?}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetDetailsDataAsync(string? barCode)
    {
        var product = await MainRepo.GetAll().FirstOrDefaultAsync(product => product.BarCode == barCode);
        if(product == null)
        {
            return BadRequest();
        }

        return Ok(product);
    }

    [HttpGet]
    public override async Task<IActionResult> CreateAsync()
    {
        return View(new Product());
    }

    //[HttpPost]
    //[ActionName("Create")]
    //[ValidateAntiForgeryToken]
    //public override async Task<IActionResult> CreateAsync()
    //{
    //    var newCar = new Car();

    //    //var newCar = new Car
    //    //{
    //    //    Color = "Purple",
    //    //    MakeId = 1,
    //    //    PetName = "Prince"
    //    //};
    //    //if (await TryUpdateModelAsync(newCar, "",
    //    //        c => c.Color, c => c.PetName, c => c.MakeId))
    //    if (await TryUpdateModelAsync(newCar, "", c => c.PetName, c => c.MakeId))
    //    {
    //        await MainDataService.AddAsync(newCar);
    //        return RedirectToAction(nameof(IndexAsync).RemoveAsyncSuffix(), new { id = newCar.Id });
    //    }

    //    var isValid = ModelState.IsValid; //false
    //    newCar.Color = "Purple";
    //    TryValidateModel(newCar);
    //    isValid = ModelState.IsValid; //still false
    //    ModelState.Clear();
    //    TryValidateModel(newCar);
    //    isValid = ModelState.IsValid; //true
    //    ViewData["MakeId"] = await GetLookupValuesAsync();
    //    return View(newCar);
    //}
}
