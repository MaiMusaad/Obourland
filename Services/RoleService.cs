using Microsoft.EntityFrameworkCore;
using ObourLand.Entities;

namespace ObourLand.Services
{
    public class RoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
