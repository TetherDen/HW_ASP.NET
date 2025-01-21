namespace HW_18_Student_Journal.Models
{

    // Модель для СВязи Груп и Студентов
    public class GroupMember
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string StudentId { get; set; }

        public Group Group { get; set; }
        public User Student { get; set; }
    }

}
