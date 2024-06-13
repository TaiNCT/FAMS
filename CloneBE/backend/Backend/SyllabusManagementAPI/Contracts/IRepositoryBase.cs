using System.Linq.Expressions;

namespace SyllabusManagementAPI.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		IQueryable<T> FindByConditionV2(Expression<Func<T, bool>> expression);
		void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
