using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Contexts;

namespace WebAPI.Data.Repositories.User
{
    public sealed class UserRepository : EfRepositoryBase<Models.User, TemplateDbContext>,IUserRepository
    {
        public UserRepository(TemplateDbContext context) : base(context)
        {
        }
    }
}