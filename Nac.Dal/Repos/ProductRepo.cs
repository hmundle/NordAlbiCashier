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

}
