using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using AppContext = App.DataAccess.Context.AppContext;

namespace App.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppContext dbContext;

        public Repository(AppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await this.dbContext.AddAsync(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                this.dbContext.Entry<T>(entity).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await this.dbContext.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<T> GetByIdWithIncludesAsync(int id, bool isNoTracking = true, params Expression<Func<T, dynamic>>[] includes)
        {
            var query = isNoTracking ? this.dbContext.Set<T>().AsNoTracking() : this.dbContext.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}