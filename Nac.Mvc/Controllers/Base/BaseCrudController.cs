
using Microsoft.EntityFrameworkCore;
using Nac.Dal.Repos.Base.IRepo;

namespace Nac.Mvc.Controllers.Base;

[Route("[controller]/[action]")]
public abstract class BaseCrudController<TEntity, TController> : Controller
where TEntity : BaseEntity, new()
where TController : class
{
    protected readonly IAppLogging<TController> AppLoggingInstance;
    protected readonly IIdRepo<TEntity> MainRepo;

    protected BaseCrudController(
        IAppLogging<TController> appLogging,
        IIdRepo<TEntity> mainRepo)
    {
        AppLoggingInstance = appLogging;
        MainRepo = mainRepo;
    }

    protected async Task<TEntity?> GetOneEntityAsync(Guid? id)
        => await MainRepo.FindAsync(id);

    [HttpGet]
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public virtual async Task<IActionResult> IndexAsync()
        => View(await MainRepo.GetAll().ToListAsync());

    [HttpGet("{id?}")]
    public virtual async Task<IActionResult> DetailsAsync(Guid? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }
        var entity = await GetOneEntityAsync(id.Value);
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    [HttpGet]
    public virtual async Task<IActionResult> CreateAsync()
    {
        // ViewData["LookupValues"] = await GetLookupValuesAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> CreateAsync(TEntity entity)
    {
        if (ModelState.IsValid)
        {
            entity.Operator = User.Identity?.Name ?? "unknown";
            await MainRepo.AddAsync(entity);
            return RedirectToAction(nameof(DetailsAsync).RemoveAsyncPostfix(), new { id = entity.Id });
        }
        // ViewData["LookupValues"] = await GetLookupValuesAsync();
        return View(entity);
    }

    [HttpGet("{id?}")]
    public virtual async Task<IActionResult> EditAsync(Guid? id)
    {
        if (!id.HasValue)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }
        var entity = await GetOneEntityAsync(id.Value);
        if (entity == null)
        {
            ViewData["Error"] = "Not Found";
            return View();
        }
        // ViewData["LookupValues"] = await GetLookupValuesAsync();
        return View(entity);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> EditAsync(Guid id, TEntity entity)
    {
        if (id != entity.Id)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }
        if (ModelState.IsValid)
        {
            // set UTC flag again, got lost in view
            entity.Created = DateTime.SpecifyKind(entity.Created!.Value, DateTimeKind.Utc);
            // set the modified data
            entity.Modified = DateTime.UtcNow;
            entity.Operator = User.Identity?.Name ?? "unknown";
            await MainRepo.UpdateAsync(entity);
            return RedirectToAction(nameof(DetailsAsync).RemoveAsyncPostfix(), new { id });
        }
        // ViewData["LookupValues"] = await GetLookupValuesAsync();
        return View(entity);
    }

    //[HttpPost("{id}")]
    //[ValidateAntiForgeryToken]
    //public virtual async Task<IActionResult> EditAsync(Guid id)
    //{
    //    var entityToUpdate = await GetOneEntityAsync(id);
    //    if (entityToUpdate == null)
    //    {
    //        return NoContent();
    //    }
    //    // use manual model binding, to avoid sending all value in
    //    // hidden inputs and due to issue with DateType.Kind == UTC
    //    if (await TryUpdateModelAsync<TEntity>(
    //        entityToUpdate,
    //        ""))
    //    {
    //        // SaveChanges would be enough, but for clearness do the Update
    //        // await _context.SaveChangesAsync();
    //        await MainRepo.UpdateAsync(entityToUpdate, persist: true);

    //        return RedirectToAction(nameof(DetailsAsync).RemoveAsyncPostfix(), new { id });
    //    }
    //    // ViewData["LookupValues"] = await GetLookupValuesAsync();
    //    return View(entityToUpdate);
    //}

    [HttpGet("{id?}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid? id)
    {
        if (!id.HasValue)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }
        var entity = await GetOneEntityAsync(id.Value);
        if (entity == null)
        {
            ViewData["Error"] = "Not Found";
            return View();
        }
        entity.Modified = DateTime.UtcNow;
        entity.IsDeleted = true;
        await MainRepo.UpdateAsync(entity, persist: true);
        return RedirectToAction(nameof(IndexAsync).RemoveAsyncPostfix());
    }

}
