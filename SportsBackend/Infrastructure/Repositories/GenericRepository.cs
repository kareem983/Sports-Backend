using AutoMapper;
using Core.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly SportsContext _context;
        internal DbSet<T> _entity;
        protected readonly IMapper mapper;
        public GenericRepository(SportsContext context, IMapper mapper)
        {
            _context = context;
            _entity = context.Set<T>();
            this.mapper = mapper;
        }

        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _entity.FindAsync(id);
        public async Task<T?> GetByGuid(Guid id) => await _entity.FindAsync(id);


        public async Task AddAsync(T entity) => await _entity.AddAsync(entity);
        public async Task AddRangeAsync(IEnumerable<T> entities) => await _entity.AddRangeAsync(entities);
        public async Task<T> AddAsync<TSource>(TSource entityDTO) where TSource : class
        {
            var entity = mapper.Map<T>(entityDTO);
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public T Add<TSource>(TSource entityDTO) where TSource : class
        {
            var entity = mapper.Map<T>(entityDTO);
            _context.Set<T>().Add(entity);
            return entity;
        }


        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;
        public void Update(string Query)
        {
            _context.Database.ExecuteSqlRaw(Query);
        }
        public void Update<TSource>(TSource entityDTO) where TSource : class
        {
            var entity = mapper.Map<T>(entityDTO);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task UpdateAsync<TSource>(TSource entityDTO) where TSource : class
        {
            var entity = mapper.Map<T>(entityDTO);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task AutoMapperUpdate<TSource>(TSource entityDTO) where TSource : class
        {
            await Task.CompletedTask;
            var entity = mapper.Map<T>(entityDTO);
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(T entity) => _context.Entry(entity).State = EntityState.Deleted;
        public async Task<bool> IsExist(Expression<Func<T, bool>> filter) => await _entity.AnyAsync(filter);
        public async Task<bool> Save() => await _context.SaveChangesAsync() > 0;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        
        public async Task<object> SqlRaw(string Query)
        {
            return await _context.Set<T>().FromSqlRaw(Query).ToListAsync();
        }


    }
}
