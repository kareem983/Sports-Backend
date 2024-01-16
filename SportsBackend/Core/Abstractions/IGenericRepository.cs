﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByGuid(Guid id);
        
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        T Add<TSource>(TSource entityDTO) where TSource : class;
        Task<T> AddAsync<TSource>(TSource entityDTO) where TSource : class;


        void Update(T entity);
        void Update(string Query);
        void Update<TSource>(TSource entityDTO) where TSource : class;
        Task UpdateAsync<TSource>(TSource entityDTO) where TSource : class;
        Task AutoMapperUpdate<TSource>(TSource entityDTO) where TSource : class;

        void Delete(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> filter);
        Task<bool> Save();
        Task SaveAsync();
        Task<object> SqlRaw(string Query);

    }
}
