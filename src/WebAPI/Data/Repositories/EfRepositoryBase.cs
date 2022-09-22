using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;

namespace WebAPI.Data.Repositories
{
    public class EfRepositoryBase<TEntity, TContext> : IEfRepositoryBase<TEntity>
    where TEntity : Entity,new()
    where TContext : DbContext
    {
        public TContext Context { get; }
        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public DbSet<TEntity> Table => Context.Set<TEntity>();

        // Read

        public IQueryable<TEntity> GetAll(bool tracking = true)
        => tracking ? Table : Table.AsNoTracking();

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true)
        => tracking ? Table.Where(method) : Table.AsNoTracking().Where(method);

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool tracking = true)
        => tracking ? Table.SingleAsync(method) : Table.AsNoTracking().SingleAsync(method);


        // Write

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> AddRangeAsync(List<TEntity> datas)
        {
            foreach (var data in datas)
            {
                Context.Entry(data).State = EntityState.Added;
            }

            await Context.SaveChangesAsync();

            return datas;
        }

        public async Task RemoveAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(List<TEntity> datas)
        {

            foreach (var data in datas)
            {
                Context.Entry(data).State = EntityState.Deleted;
            }

            await Context.SaveChangesAsync();

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task SaveChangesAsync()=>await Context.SaveChangesAsync();
    }
}