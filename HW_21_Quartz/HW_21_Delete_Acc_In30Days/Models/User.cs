namespace HW_21_Delete_Acc_In30Days.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
