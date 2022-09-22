using Microsoft.AspNetCore.Identity;
using WebAPI.Data.Contexts;
using WebAPI.Models;

namespace WebAPI.Data.Repositories
{
    public sealed class AppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TemplateDbContext _context;

        public AppUserRepository(UserManager<AppUser> userManager, TemplateDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        public async Task<AppUser> CreateAppUserAsync(AppUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<AppUser> UpdateAppUserAsync(AppUser user)
        {
            var userFromDb = await _userManager.FindByIdAsync(user.Id);
            if (userFromDb == null)
            {
                throw new Exception("You can not update because user does not exist");
            }

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<AppUser> GetAppUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<AppUser> GetAppUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<AppUser> GetAppUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task DeleteAppUserByIdAsync(string id)
        {
            AppUser user = await GetAppUserByIdAsync(id);

            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppUserByUserNameAsync(string userName)
        {
            AppUser user = await GetAppUserByUserNameAsync(userName);

            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppUserByEmailAsync(string email)
        {
            AppUser user = await GetAppUserByEmailAsync(email);

            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();
        }
    }
}
