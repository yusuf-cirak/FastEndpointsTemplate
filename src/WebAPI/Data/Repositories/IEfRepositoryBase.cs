using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;

namespace WebAPI.Data.Repositories
{
    public interface IEfRepositoryBase<TEntity>
    where TEntity : Entity,new()
    {

        public DbSet<TEntity> Table {get;}


        // Read
        IQueryable<TEntity> GetAll(bool tracking = true);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool tracking = true);


        // Write

        Task<TEntity> AddAsync(TEntity entity);
        Task<List<TEntity>> AddRangeAsync(List<TEntity> datas);
        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(List<TEntity> datas);

        Task<TEntity> UpdateAsync(TEntity entity);


        Task SaveChangesAsync();

    }
}