using Nac.Dal.Repos.Base;
using Nac.Dal.Repos.Interfaces;

namespace Nac.Dal.Repos;

public class UserRepo : BaseEntityRepo<User>, IUserRepo
{
    public UserRepo(NacDbContext context)
    : base(context)
    {
    }

    internal UserRepo(DbContextOptions<NacDbContext> options)
    : base(options)
    {
    }
}
