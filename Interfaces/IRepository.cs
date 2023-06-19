using System;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace TestSchadApp.Interfaces
{
	public interface IRepository<T>
	{
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(int id);
        Task RemoveAsync(string property, int id);
        Task RemoveAsync(T entity);
    }
}

