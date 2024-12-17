using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW_11.ViewModels
{
    public record RegisterViewModel
    {

        [CreditCard(ErrorMessage ="Invalid Credit Card")]
        public string CreditCard { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Phone]
        public string PhoneNumber { get; set; }

        [Range(minimum: 14, maximum: 120)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]*$")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "UserName length from 4 to 20 chars")]
        [Remote(action: "IsUsernameAvailable", controller: "Account", ErrorMessage ="This Username allready taken")]
		public string UserName {  get; set; }

        [Url]
        public string Website {  get; set; }


    }
}
