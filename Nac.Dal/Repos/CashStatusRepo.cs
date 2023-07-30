using Nac.Dal.Exceptions;
using Nac.Dal.Repos.Base;
using Nac.Dal.Repos.Interfaces;
using Nac.Models.Utilities;

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
