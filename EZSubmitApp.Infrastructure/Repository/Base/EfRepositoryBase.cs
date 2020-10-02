using EZSubmitApp.Core.Entities.Base;
using EZSubmitApp.Core.IRepositories.Base;
using EZSubmitApp.Core.Specifications.Base;
using EZSubmitApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EZSubmitApp.Infrastructure.Repository.Base
{
    public class EfRepositoryBase<T, TId> : IAsyncRepositoryBase<T, TId> where T : class, IBaseEntity<TId>
    {
        public EfRepositoryBase(EZSubmitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected readonly EZSubmitDbContext _dbContext;
        private DbSet<T> _entities;

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbContext.Set<T>();
                
                return _entities;
            }
        }

        public IQueryable<T> Table => Entities;

        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        public async virtual Task<T> GetByIdAsync(TId id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await Entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> FirstAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<bool> IfExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entities.AnyAsync(predicate);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }

        /// <summary>
        /// Return the result of a query using the specification's criteria expression.
        /// </summary>
        /// <param name="spec">The specification that describes the shape of the data
        /// to be returned.</param>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T, TId>.GetQuery(Table, spec);
        }
    }
}
