using Microsoft.EntityFrameworkCore;
using ObourLand.Entities;
using ObourLand.Models;

namespace ObourLand.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAll()
        {
            return await _context.Users.Where(w => w.IsActive == true).Select(s => new UserDto
            {
                UserId = s.Id,
                UserName = s.UserName,
                FirstName = s.FirstName,
                LastName = s.LastName,
                GroupName = s.Group.Name,
                RoleName = s.Role.Name,
            }).ToListAsync();
        }

        public async Task<GroupedUsersDto?> GetByGroup(int groupId)
        {
            var res = await _context.Users.Where(w => w.IsActive == true && w.GroupId == groupId && groupId > 0 && w.Group.IsActive == true)
                                        .GroupBy(g => g.GroupId)
                                       .Select(s => new GroupedUsersDto
                                       {
                                           GroupId = s.Key.Value,
                                           GroupName = $"{s.Max(s => s.Group.Name)}",
                                           Users = s.Select(ss => new UserDto
                                           {
                                               UserId = ss.Id,
                                               UserName = ss.UserName,
                                               FirstName = ss.FirstName,
                                               LastName = ss.LastName,
                                               RoleName = ss.Role.Name,
                                               GroupName = ss.Group.Name,
                                           }).ToList()
                                       }).FirstOrDefaultAsync();
            return res;
        }

        //public async Task<List<UserDto>> GetSupervisors()
        //{
        //    return await _context.Users.Where(w => w.IsActive == true && w.RoleId == 2).Select(s => new UserDto
        //    {
        //        UserId = s.Id,
        //        UserName = s.UserName,
        //        FirstName = s.FirstName,
        //        LastName = s.LastName,
        //        RoleName = s.Role.Name,
        //    }).ToListAsync();
        //}

        public async Task<User> CheckUser(string userName, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(w => w.UserName == userName && password == password && w.IsActive == true);
        }

        public async Task<User> Create(RegisterDto register)
        {
            var user = new User() { UserName = register.UserName, Password = register.Password, FirstName = register.FirstName, LastName = register.LastName, IsActive = true, 
                RoleId = register.RoleId, GroupId = (register.GroupId == 0 ? null: register.GroupId) };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<int> ActivateUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return 0;
            }
            user.IsActive = true;
            user.LastModifiedOn = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        public async Task<int> DeactivateUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return 0;
            }
            user.IsActive = false;
            user.LastModifiedOn = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
