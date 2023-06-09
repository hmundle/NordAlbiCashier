using Nac.Dal.Exceptions;
using Nac.Dal.Repos.Base.IRepo;

namespace Nac.Dal.Repos.Base;

public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
{
    private readonly bool _disposeContext;

    public NacDbContext Context { get; }

    public DbSet<T> Table { get; }

    protected BaseRepo(NacDbContext context)
    {
        Context = context;
        Table = Context.Set<T>();
        _disposeContext = false;
    }

    protected BaseRepo(DbContextOptions<NacDbContext> options) : this(new NacDbContext(options))
    {
        _disposeContext = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool _isDisposed;

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }
        if (disposing)
        {
            if (_disposeContext)
            {
                Context.Dispose();
            }
        }
        _isDisposed = true;
    }

    ~BaseRepo()
    {
        Dispose(false);
    }

    public async ValueTask<int> SaveChangesAsync()
    {
        try
        {
            return await Context.SaveChangesAsync();
        }
        catch (CustomException /*ex*/)
        {
            //Should handle intelligently - already logged
            throw;
        }
        catch (Exception ex)
        {
            //Should log and handle intelligently
            throw new CustomException("An error occurred updating the database", ex);
        }
    }

    public virtual IQueryable<T> GetAll() => Table;
    public virtual IQueryable<T> GetAllEvenDeleted() => Table.IgnoreQueryFilters();

    //public void ExecuteQuery(string sql, object[] sqlParametersObjects) => Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);

    public virtual async ValueTask<int> AddAsync(T entity, bool persist = true)
    {
        await Table.AddAsync(entity);
        return persist ? await SaveChangesAsync() : 0;
    }
    public virtual async ValueTask<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true)
    {
        await Table.AddRangeAsync(entities);
        return persist ? await SaveChangesAsync() : 0;
    }
    public virtual async ValueTask<int> UpdateAsync(T entity, bool persist = true)
    {
        Table.Update(entity);
        return persist ? await SaveChangesAsync() : 0;
    }
    public virtual async ValueTask<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true)
    {
        Table.UpdateRange(entities);
        return persist ? await SaveChangesAsync() : 0;
    }
    public virtual async ValueTask<int> DeleteAsync(T entity, bool persist = true)
    {
        Table.Remove(entity);
        return persist ? await SaveChangesAsync() : 0;
    }
    public virtual async ValueTask<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true)
    {
        Table.RemoveRange(entities);
        return persist ? await SaveChangesAsync() : 0;
    }

}
