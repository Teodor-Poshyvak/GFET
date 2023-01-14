using System.ComponentModel.DataAnnotations;

namespace GFET.Enum;

public enum UserRole
{
    [Display(Name = "Пользователь")]
    User = 0,
    [Display(Name = "Кур'єр")]
    Moderator = 1,
    [Display(Name = "Админ")]
    Admin = 2,

}