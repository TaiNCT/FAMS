using Entities.Models;
using System.Linq.Expressions;

namespace ReservationManagementAPI.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> FindAllAsync();
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<bool> CreateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> SaveChange();

    }
}
