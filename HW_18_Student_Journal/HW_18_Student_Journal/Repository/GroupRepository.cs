using HW_18_Student_Journal.Data;
using HW_18_Student_Journal.Interface;
using HW_18_Student_Journal.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HW_18_Student_Journal.Repository
{
    public class GroupRepository : IGroup
    {
        private readonly ApplicationContext _context;

        public GroupRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Group> AddGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            return await _context.Groups
                .Include(g => g.GroupMembers)
                .ThenInclude(gm => gm.Student)
                .Include(g => g.Subjects)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task AddStudentsToGroupAsync(int groupId, List<string> studentIds)
        {
            var group = await _context.Groups.Include(g => g.GroupMembers).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null) return;

            foreach (var studentId in studentIds)
            {
                // проверка что студент не был уже добавлен в группу
                if (!_context.GroupMembers.Any(gm => gm.GroupId == groupId && gm.StudentId == studentId))
                {
                    _context.GroupMembers.Add(new GroupMember
                    {
                        GroupId = groupId,
                        StudentId = studentId
                    });
                }
            }
            await _context.SaveChangesAsync();
        }



        public async Task<List<User>> GetStudentsWithoutGroupsAsync(int groupId)
        {
            var existingStudentIds = await _context.GroupMembers
                .Where(gm => gm.GroupId == groupId)
                .Select(gm => gm.StudentId)
                .ToListAsync();

            return await _context.Users
                .Where(u => !existingStudentIds.Contains(u.Id))
                .ToListAsync();
        }

        public async Task<bool> IsGroupExistsAsync(string name)
        {
            return await _context.Groups
                .AnyAsync(g => g.Name.ToLower() == name.ToLower());
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddGroupMemberAsync(GroupMember groupMember)
        {
            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGroupMemberAsync(GroupMember groupMember)
        {
            _context.GroupMembers.Remove(groupMember);
            await _context.SaveChangesAsync();
        }

        public async Task<GroupMember> GetGroupMemberLinkAsync(int groupId, string studentId)
        {
            return await _context.GroupMembers.FirstOrDefaultAsync(gm => gm.GroupId == groupId && gm.StudentId == studentId);
        }

        //====================================================================

        // Subjects...
        public async Task AddSubjectToGroupAsync(int groupId, int subjectId)
        {
            var group = await _context.Groups.Include(g => g.Subjects).FirstOrDefaultAsync(g => g.Id == groupId);
            var subject = await _context.Subjects.FindAsync(subjectId);

            if (group == null || subject == null)
            {
                return;
            }

            if (!group.Subjects.Contains(subject))
            {
                group.Subjects.Add(subject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveSubjectFromGroupAsync(int groupId, int subjectId)
        {
            var group = await _context.Groups.Include(g => g.Subjects).FirstOrDefaultAsync(g => g.Id == groupId);
            var subject = await _context.Subjects.FindAsync(subjectId);

            if (group == null || subject == null)
            {
                return;
            }

            group.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }



    }
}
