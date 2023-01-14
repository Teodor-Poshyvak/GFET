using System.ComponentModel.DataAnnotations;

namespace GFET.Models;

public class ChangePassViewModel
{
    public string UserName { get; set; }
        
    [Required(ErrorMessage = "Введіть пароль!")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    [MinLength(5, ErrorMessage = "Пароль повинен мати 6 символів АБО більше!")]
    public string NewPassword { get; set; }
}