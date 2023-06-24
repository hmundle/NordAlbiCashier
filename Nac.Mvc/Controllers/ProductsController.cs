using Nac.Dal.Repos.Interfaces;
using Nac.Models.Entities;
using Nac.Mvc.Controllers.Base;

namespace Nac.Mvc.Controllers;

public class ProductsController : BaseCrudController<Product, ProductsController>
{
    //private readonly IMakeDataService _lookupDataService;

    public ProductsController(IAppLogging<ProductsController> appLogging, IProductRepo mainRepo) 
    : base(appLogging, mainRepo)
    {
    }

    // protected override async Task<SelectList> GetLookupValuesAsync()
    //     => new SelectList(await _lookupDataService.GetAllAsync(), nameof(Make.Id), nameof(Make.Name));


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
