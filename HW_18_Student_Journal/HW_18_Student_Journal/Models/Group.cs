namespace HW_18_Student_Journal.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupMember> GroupMembers { get; set; } // список студентов в группе через связь модель связи ГрупМембер
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();  //  Преподоваемые предметы в группе
    }
}
