using Microsoft.EntityFrameworkCore;
using ObourLand.Entities;
using ObourLand.Enums;
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

        public async Task<UserDto?> GetById(int id)
        {
            return await _context.Users.Where(w => w.IsActive == true && w.Id == id)
                                       .Select(s => new UserDto {
                                              UserId = s.Id,
                                              UserName = s.UserName,
                                              FirstName = s.FirstName,
                                              LastName = s.LastName,
                                              GroupName = s.Group.Name,
                                              RoleName = s.Role.Name,
                                       }).FirstOrDefaultAsync();
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

        public async Task<List<UserDto>> GetSupervisors()
        {
            var users = await _context.Users.Where(w => w.IsActive == true && w.RoleId == (int)UserRoles.Supervisor)
                                       .Select(s => new UserDto
                                        {
                                            UserId = s.Id,
                                            UserName = s.UserName,
                                            FirstName = s.FirstName,
                                            LastName = s.LastName,
                                            RoleName = s.Role.Name,
                                        }).ToListAsync();

            return users;
        }

        public async Task<List<UserDto>> GetAssignedUsers(int supervisorId)
        {
            var users = await _context.Users.Where(w => w.IsActive == true && w.SupervisorId == supervisorId)
                                            .Select(s => new UserDto
                                            {
                                                UserId = s.Id,
                                                UserName = s.UserName,
                                                FirstName = s.FirstName,
                                                LastName = s.LastName,
                                                GroupName = s.Group.Name,
                                                RoleName = s.Role.Name,
                                            }).ToListAsync();
            return users;
        }

        public async Task<Result<bool>> AssignedUsers(int supervisorId, List<int> userIds)
        {
            try
            {
                var users = await _context.Users.Where(w => userIds.Contains(w.Id)).ToListAsync();
                users = users.Select(s =>
                {
                    s.SupervisorId = supervisorId;
                    return s;
                }).ToList();

                _context.Users.UpdateRange(users);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Users assigned to supervisor successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("Failed to assign users to supervisor.");
            }
        }

        public async Task<User> CheckUser(string userName, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(w => w.UserName == userName && password == password && w.IsActive == true);
        }

        public async Task<User> Create(RegisterDto register)
        {
            var user = new User() { 
                UserName = register.UserName,
                Password = register.Password,
                FirstName = register.FirstName,
                LastName = register.LastName,
                IsActive = true, 
                RoleId = register.RoleId,
                GroupId = (register.GroupId == 0 ? null: register.GroupId) 
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<Result<bool>> ActivateUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    return Result<bool>.Failure("This user not found.");
                }
                user.IsActive = true;
                user.LastModifiedOn = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Users activated successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("Failed to activate user.");
            }
        }

        public async Task<Result<bool>> DeactivateUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    return Result<bool>.Failure("This user not found.");
                }
                user.IsActive = false;
                user.LastModifiedOn = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Users Deactivated successfully.");
            } catch (Exception ex)
            {
                return Result<bool>.Failure("Failed to Deactivate user.");
            }
        }

    }
}
