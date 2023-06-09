using Nac.Models.Utilities;

namespace Nac.Dal.Repos.Base.IRepo;

public interface IIdRepo<T> : IRepo<T> where T : BaseEntity
{
    ValueTask<int> DeleteAsync(Guid id, uint _xmin, bool persist = true);
    Task<T?> FindAsync(Guid? id);
    //Task<T?> FindWithProcAsync(Guid? id);
    IQueryable<T> FindAsQuery(Guid? id);
    Task<T?> FindAsNoTrackingAsync(Guid id);
    Task<T?> FindEvenDeletedAsync(Guid id);
    ValueTask<bool> AnyAsync(Guid id);
    ValueTask<bool> AnyEvenDeletedAsync(Guid id);
}
