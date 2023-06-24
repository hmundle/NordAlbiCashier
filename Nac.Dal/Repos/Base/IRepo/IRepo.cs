namespace Nac.Dal.Repos.Base.IRepo;

public interface IRepo<T> : IDisposable
{
    NacDbContext Context { get; }
    ValueTask<int> AddAsync(T entity, bool persist = true);
    ValueTask<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true);
    ValueTask<int> UpdateAsync(T entity, bool persist = true);
    ValueTask<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true);
    ValueTask<int> DeleteAsync(T entity, bool persist = true);
    ValueTask<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true);
    IQueryable<T> GetAll();
    IQueryable<T> GetAllEvenDeleted();
    //void ExecuteQuery(string sql, object[] sqlParametersObjects);

    ValueTask<int> SaveChangesAsync();

}
