﻿using EZSubmitApp.Core.Entities.Base;
using EZSubmitApp.Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.IRepositories.Base
{
    public interface IAsyncRepositoryBase<T, TId> : IRepository where T : IBaseEntity<TId>
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }

        Task<T> GetByIdAsync(TId id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> FirstAsync(ISpecification<T> spec);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);

        Task<bool> IfExistsAsync(Expression<Func<T, bool>> predicate);
        Task<bool> SaveAsync();
    }
}
