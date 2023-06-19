using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestSchadApp.Persistances;
using TestSchadApp.Interfaces;

namespace TestSchadApp.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public SqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateAsync(T entity)
        {
            _context.Entry(entity).Context.Add(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? _context.Set<T>().AsNoTracking() : _context.Set<T>().AsNoTracking().Where(filter);
            var notSortedResults = transform(query);
            return await notSortedResults.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<T> GetAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? _context.Set<T>().AsNoTracking() : _context.Set<T>().AsNoTracking().Where(filter);
            var notSortedResults = transform(query);
            return await notSortedResults.FirstOrDefaultAsync();
        }

        public Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).Context.Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(int id)
        {
            var entity = Activator.CreateInstance<T>();
            var prop = typeof(T).GetProperty("Id");
            prop!.SetValue(entity, id, null);

            _context.Entry(entity).Context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string property, int id)
        {
            var entity = Activator.CreateInstance<T>();
            var prop = typeof(T).GetProperty(property);
            prop!.SetValue(entity, id, null);

            _context.Entry(entity).Context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity)
        {
            _context.Entry(entity).Context.Remove(entity);
            return Task.CompletedTask;
        }
    }
}