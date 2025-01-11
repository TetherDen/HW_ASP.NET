using System.ComponentModel.DataAnnotations;

namespace HW_15_Email_FutureMe.Models
{
    public class FutureLetter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please write your letter")]
        [MinLength(10, ErrorMessage = "Letter should be at least 10 characters long")]
        public string Content { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool IsSent { get; set; }

        public DateTime? SentDate { get; set; }
    }
}
