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

}
