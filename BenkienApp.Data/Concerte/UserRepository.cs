
using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BenkienApp.Data.Concerte
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUserByIncludeAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> GetUserByIncludeAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Role)
                .FirstOrDefaultAsync();
        }
    }
}
