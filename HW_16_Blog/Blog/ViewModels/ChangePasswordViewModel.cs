using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Введите свой пароль")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Подтвердите новый пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
