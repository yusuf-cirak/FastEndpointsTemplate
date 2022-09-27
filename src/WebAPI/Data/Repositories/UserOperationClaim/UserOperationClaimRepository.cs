using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Contexts;
using WebAPI.Models;

namespace WebAPI.Data.Repositories
{
    public sealed class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, TemplateDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(TemplateDbContext context) : base(context)
        {
        }
    }
}