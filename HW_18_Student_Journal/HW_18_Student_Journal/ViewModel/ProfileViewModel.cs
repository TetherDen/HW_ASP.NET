using System.ComponentModel.DataAnnotations;

namespace HW_18_Student_Journal.ViewModel
{
    public class ProfileViewModel
    {
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(256, ErrorMessage = "Email cannot be longer than 256 characters.")]
        public string Email { get; set; }

        public string? UserName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "PhoneNumber cannot be longer than 15 digits.")]
        public string? PhoneNumber { get; set; }
    }

}
