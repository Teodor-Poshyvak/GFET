using System.ComponentModel.DataAnnotations;

namespace GFET.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введіть ім'я!")]
    [MaxLength(20, ErrorMessage = "Доступно лише 20 символів!")]
    [MinLength(3, ErrorMessage = "Ім'я повинно бути більше 3-х символів!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Введіть пароль!")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}