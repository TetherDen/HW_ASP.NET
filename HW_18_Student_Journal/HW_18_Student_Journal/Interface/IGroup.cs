using HW_18_Student_Journal.Models;

namespace HW_18_Student_Journal.Interface
{
    public interface IGroup
    {
        Task<Group> AddGroupAsync(Group group);

        Task<List<Group>> GetAllGroupsAsync();
        Task<Group> GetGroupByIdAsync(int groupId);
        Task AddStudentsToGroupAsync(int groupId, List<string> studentIds);
        Task<List<User>> GetStudentsWithoutGroupsAsync(int groupId);

        Task<bool> IsGroupExistsAsync(string name);

        Task DeleteGroupAsync(int groupId);

        Task AddGroupMemberAsync(GroupMember groupMember);
        Task RemoveGroupMemberAsync(GroupMember groupMember);
        Task<GroupMember> GetGroupMemberLinkAsync(int groupId, string studentId);  // <<-- Достает связь юзера(студента) с группой которая в таблице GroupMember

        // Subjects
        Task AddSubjectToGroupAsync(int groupId, int subjectId);
        Task RemoveSubjectFromGroupAsync(int groupId, int subjectId);

    }
}
