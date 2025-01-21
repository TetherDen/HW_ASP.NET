using Microsoft.AspNetCore.Identity;

namespace HW_18_Student_Journal.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
