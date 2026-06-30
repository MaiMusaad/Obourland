using Microsoft.EntityFrameworkCore;
using ObourLand.Entities;
using ObourLand.Models;

namespace ObourLand.Services
{
    public class GroupService
    {
        private readonly AppDbContext _context;

        public GroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GroupDto>> Get()
        {
            return await _context.Groups.Where(w => w.IsActive == true).Select(s => new GroupDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }

        public async Task Create(GroupDto group)
        {
            _context.Groups.Add(new Group {Name = group.Name, IsActive = true});
            await _context.SaveChangesAsync();
        }

        public async Task Update(GroupDto group) {
            _context.Groups.Update(new Group {
               Id= group.Id, Name = group.Name, IsActive = true , LastModifiedOn = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        public async Task<int> Activate(int id)
        {
           Group? group = _context.Groups.Find(id);
            if (group == null) {
                return -1;
            }
            group.IsActive = true;
            group.LastModifiedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> Deactivate(int id)
        {
            Group? group = _context.Groups.Find(id);
            if (group == null)
            {
                return -1;
            }
            group.IsActive = false;
            group.LastModifiedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            return 0;
        }

    }
}
