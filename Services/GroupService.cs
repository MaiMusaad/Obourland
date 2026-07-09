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

        public async Task<List<UserDto>> GetUsersByGroup(int groupId)
        {

           var users = await _context.Users.Where(w => w.GroupId == groupId)
                .Select(s => new UserDto
                {
                    UserId = s.Id,
                    UserName = s.UserName,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupName = s.Group != null ? s.Group.Name : null,
                    RoleName = s.Role != null ? s.Role.Name : null,
                    SupervisorName = s.Supervisor != null ? $"{s.Supervisor.UserName}" : null
                })
                .ToListAsync();

            return users;
        }

        public async Task<Result<bool>> Create(GroupDto group)
        {
            try
            {
                _context.Groups.Add(new Group { Name = group.Name, IsActive = true, CreatedOn = DateTime.UtcNow  });
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Group created successfully.");
            }catch(Exception ex)
            {
                return Result<bool>.Failure("Failed to create group.");
            }
        }

        public async Task<Result<bool>> Update(GroupDto request) 
        {
            try
            {
                var group = await _context.Groups.FirstOrDefaultAsync(f => f.Id == request.Id);
                if(group == null)
                    return Result<bool>.Failure("Group not found.");

                group.Name = request.Name;
                group.LastModifiedOn = DateTime.UtcNow;
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Group updated successfully.");

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("Failed to update group.");
            }
        }

        public async Task<Result<bool>> Activate(int id)
        {
           Group? group = _context.Groups.Find(id);
            if (group == null) {
                return Result<bool>.Failure("This group not found.");
            }
            group.IsActive = true;
            group.LastModifiedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true, "Group activated successfully.");
        }

        public async Task<Result<bool>> Deactivate(int id)
        {
            Group? group = _context.Groups.Find(id);
            if (group == null)
            {
                return Result<bool>.Failure("This group not found.");
            }
            group.IsActive = false;
            group.LastModifiedOn = DateTime.Now;
            _context.Groups.Update(group);

            var users = await _context.Users.Where(w => w.GroupId == id).ToListAsync();
            users = users.Select(s =>
            {
                s.GroupId = null;
                return s;
            }).ToList();
            _context.Users.UpdateRange(users);

            await _context.SaveChangesAsync();
            return Result<bool>.Success(true, "Group deactivated successfully.");
        }

        public async Task<Result<bool>> AssignedUsersToGroup(int groupId, List<int> userIds)
        {
            try
            {
                var users = await _context.Users.Where(w => userIds.Contains(w.Id)).ToListAsync();
                users = users.Select(s =>
                {
                    s.GroupId = groupId;
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
    }
}
