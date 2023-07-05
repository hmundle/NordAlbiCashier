using Nac.Dal.Repos.Base;
using Nac.Dal.Repos.Interfaces;

namespace Nac.Dal.Repos;

public class InvoiceRepo : BaseEntityRepo<Invoice>, IInvoiceRepo
{
    public InvoiceRepo(NacDbContext context)
    : base(context)
    {
    }

    internal InvoiceRepo(DbContextOptions<NacDbContext> options)
    : base(options)
    {
    }

    public override Task<Invoice?> FindAsNoTrackingAsync(Guid id)
    {
        throw new NotImplementedException();
        //return base.FindAsNoTrackingAsync(id);
    }

    public override IQueryable<Invoice> FindAsQuery(Guid? id)
    {
        return base.FindAsQuery(id).Include(i => i.Sellings).ThenInclude(s => s.ProductNavigation);
    }

    public override async Task<Invoice?> FindAsync(Guid? id)
    {
        var invoice = await FindAsQuery(id).FirstOrDefaultAsync();
        return invoice;
    }
}
