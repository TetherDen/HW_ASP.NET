using System.ComponentModel.DataAnnotations;

namespace HW_15_Email_FutureMe.ViewModel
{
    public class EmailFormModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        public IFormFileCollection? Attachments { get; set; }
    }
}
