using HW_18_Student_Journal.Models;

namespace HW_18_Student_Journal.ViewModel
{
    namespace HW_18_Student_Journal.ViewModel
    {
        public class GroupDetailsViewModel
        {
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public List<User> Students { get; set; }
            public List<Subject> Subjects { get; set; }
        }
    }

}
