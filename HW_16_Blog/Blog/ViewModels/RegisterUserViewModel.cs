using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class RegisterUserViewModel
    {
        public string? Name { get; set; }

        [Required(ErrorMessage = "Не указан Email адрес")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        [Required(ErrorMessage = "номер телефона обязателен для заполнения.")]
        [Display(Name = "Номер телефона")]
        public string? Phone {  get; set; }

        [Required(ErrorMessage = "пароль обязателен для заполнения.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MinLength(4,ErrorMessage = "Пароль должен быть не менее 4 символов.")]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }


        public string? Code { get; set; }
    }
}
