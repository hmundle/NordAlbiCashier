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
}
