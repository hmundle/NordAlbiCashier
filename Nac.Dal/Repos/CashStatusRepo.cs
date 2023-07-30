using Nac.Dal.Repos.Base;
using Nac.Dal.Repos.Interfaces;

namespace Nac.Dal.Repos;

public class CashStatusRepo : BaseEntityRepo<CashStatus>, ICashStatusRepo
{
    public CashStatusRepo(NacDbContext context)
    : base(context)
    {
    }

    internal CashStatusRepo(DbContextOptions<NacDbContext> options)
    : base(options)
    {
    }

}
