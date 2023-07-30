using Nac.Dal.Repos.Base;
using Nac.Dal.Repos.Interfaces;

namespace Nac.Dal.Repos;

public class CashFlowRepo : BaseEntityRepo<CashFlow>, ICashFlowRepo
{
    public CashFlowRepo(NacDbContext context)
    : base(context)
    {
    }

    internal CashFlowRepo(DbContextOptions<NacDbContext> options)
    : base(options)
    {
    }

}
