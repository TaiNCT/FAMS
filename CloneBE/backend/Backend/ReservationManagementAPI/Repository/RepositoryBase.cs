using Microsoft.EntityFrameworkCore;
using ReservationManagementAPI.Contracts;
using Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Context;

namespace ReservationManagementAPI.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected FamsContext RepositoryContext { get; set; }
        public RepositoryBase(FamsContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public Task<List<T>> FindAllAsync() => RepositoryContext.Set<T>().AsNoTracking().ToListAsync();
        public Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression) =>
            RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();


        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await RepositoryContext.Set<T>().AddAsync(entity);
                return await SaveChange();
            } catch (Exception ex)
            {
                throw new Exception("Fail to add",ex);
            }
        }


        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                RepositoryContext.Set<T>().Update(entity);
                return await SaveChange();
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to update", ex);
            }
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                RepositoryContext.Set<T>().Remove(entity);
                return await SaveChange();
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to remove", ex);
            }
        }

        public async Task<bool> SaveChange()
        {
            var result = await RepositoryContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
