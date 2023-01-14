using System.ComponentModel.DataAnnotations;

namespace GFET.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Вкажіть ім'я!")]
    [MaxLength(20, ErrorMessage = "Доступно лише 20 символів!")]
    [MinLength(3, ErrorMessage = "Ім'я повинно бути більше 3-х символів!")]
    public string Name { get; set; }
        
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Введіть пароль!")]
    [MinLength(6, ErrorMessage = "Пароль повинен мати більше 6-ти символів!")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Підтвердіть пароль")]
    [Compare("Password", ErrorMessage = "Паролі не збігаються!")]
    public string PasswordConfirm { get; set; }
}