using System.ComponentModel.DataAnnotations;

namespace HW_13.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
