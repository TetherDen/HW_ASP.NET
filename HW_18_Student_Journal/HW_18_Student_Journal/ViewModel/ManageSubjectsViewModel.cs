using HW_18_Student_Journal.Models;

namespace HW_18_Student_Journal.ViewModel
{
    public class ManageSubjectsViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<Subject> AvailableSubjects { get; set; }
        public IEnumerable<Subject> AssignedSubjects { get; set; }
    }

}
