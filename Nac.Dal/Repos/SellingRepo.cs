using Nac.Dal.Exceptions;
using Nac.Dal.Repos.Base;
using Nac.Dal.Repos.Interfaces;
using Nac.Models.Utilities;

namespace Nac.Dal.Repos;

public class SellingRepo : BaseEntityRepo<Selling>, ISellingRepo
{
    public SellingRepo(NacDbContext context)
    : base(context)
    {
    }

    internal SellingRepo(DbContextOptions<NacDbContext> options)
    : base(options)
    {
    }

    // public virtual async Task<L0Product?> FindWithProcAsync(Guid? ProcId)
    // {
    //     var product = await Table
    //         .Where(p => p.ProcId == ProcId)
    //         .Include(p => p.L0Archivings)
    //         .Include(p => p.L0Deliveries)
    //         .Include(p => p.L0QualityControls)
    //         .Include(p => p.L0Sips)
    //         .Include(p => p.ProcErrorNavigation)
    //         .FirstOrDefaultAsync();
    //     return product;
    // }

}
