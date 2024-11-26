namespace HW_05.Models
{
    public class CustomerService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string UserToken { get; set; }
        public User User { get; set; }
    }
}
