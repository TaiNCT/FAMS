using Microsoft.EntityFrameworkCore;
using SyllabusManagementAPI.Contracts;
using Entities.Models;
using System.Linq.Expressions;
using Entities.Context;

namespace SyllabusManagementAPI.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected FamsContext _famsContext { get; set; }
        public RepositoryBase(FamsContext repositoryContext)
        {
            _famsContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => _famsContext.Set<T>().AsNoTracking();

        /// <summary>
        /// Finds entities in the database that satisfy the specified condition.
        /// </summary>
        /// <param name="expression">The condition to be satisfied.</param>
        /// <returns>An IQueryable of entities that satisfy the condition.</returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _famsContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _famsContext.Set<T>().Add(entity);
        public void Update(T entity) => _famsContext.Set<T>().Update(entity);
        public void Delete(T entity) => _famsContext.Set<T>().Remove(entity);

		public IQueryable<T> FindByConditionV2(Expression<Func<T, bool>> expression) => _famsContext.Set<T>().Where(expression);
	}
}
