using System.ComponentModel.DataAnnotations;

namespace GFET.Models.User;

public class UserViewModel
{
    [Display(Name = "Id")]
    public long Id { get; set; }
        
    [Required(ErrorMessage = "Введіть роль!")]
    [Display(Name = "Роль")]
    public string Role { get; set; }
        
    [Required(ErrorMessage = "Вкажіть логін!")]
    [Display(Name = "Логін")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Введіть пароль!")]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}