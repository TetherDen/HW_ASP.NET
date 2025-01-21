using HW_18_Student_Journal.Models;

namespace HW_18_Student_Journal.ViewModel
{
    public class ManageStudentsViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<User> StudentsInGroup { get; set; }
        public List<User> AvailableStudents { get; set; }
    }
}
