using Nac.Dal.Exceptions;
using Nac.Dal.Repos.Base;
using Nac.Dal.Repos.Interfaces;
using Nac.Models.Utilities;

namespace Nac.Dal.Repos;

public class ProductRepo : BaseEntityRepo<Product>, IProductRepo
{
    public ProductRepo(NacDbContext context)
    : base(context)
    {
    }

    internal ProductRepo(DbContextOptions<NacDbContext> options)
    : base(options)
    {
    }

    // public virtual async Task<TProduct?> FindWithProcAsync(Guid? ProcId)
    // {
    //     var product = await Table
    //         .Where(p => p.ProcId == ProcId)
    //         .Include(p => p.PassNavigation).ThenInclude(p => p!.TapeNavigation)
    //         .Include(p => p.ProcErrorNavigation)
    //         .FirstOrDefaultAsync();
    //     return product;
    // }

}
