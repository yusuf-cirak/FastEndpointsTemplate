using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebAPI.Models.Common;

namespace WebAPI.Data.Repositories
{
    public interface IEfRepositoryBase<TEntity>
    where TEntity : Entity, new()
    {

        IQueryable<TEntity> Query();

        // Read
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
        include = null,
         bool enableTracking = true);

        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                         bool enableTracking = true,
                                        CancellationToken cancellationToken = default);


        // Write

        Task<TEntity> AddAsync(TEntity entity);
        Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);


    }
}