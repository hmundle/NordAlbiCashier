namespace Nac.Dal.Repos.Base;

public abstract class BaseEntityRepo<T> : BaseRepo<T> where T : BaseEntity, new()
{
    protected BaseEntityRepo(NacDbContext context) : base(context) { }
    protected BaseEntityRepo(DbContextOptions<NacDbContext> options) : base(options) { }

    public virtual async Task<T?> FindAsync(Guid? id) => await Table.FindAsync(id);

    public virtual IQueryable<T> FindAsQuery(Guid? id) => GetAll().Where(p => p.Id == id);

    public virtual async Task<T?> FindAsNoTrackingAsync(Guid id) => await Table.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<T?> FindEvenDeletedAsync(Guid id) => await Table.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);

    public async ValueTask<bool> AnyAsync(Guid id) => await Table.AnyAsync(x => x.Id == id);

    public async ValueTask<bool> AnyEvenDeletedAsync(Guid id) => await Table.IgnoreQueryFilters().AnyAsync(x => x.Id == id);

    public async ValueTask<int> DeleteAsync(Guid id, uint _xmin, bool persist = true)
    {
        var entity = new T { Id = id, xmin = _xmin };
        Context.Entry(entity).State = EntityState.Deleted;
        return persist ? await SaveChangesAsync() : 0;
    }
}
