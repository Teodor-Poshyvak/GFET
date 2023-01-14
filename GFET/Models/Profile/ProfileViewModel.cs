using System.ComponentModel.DataAnnotations;

namespace GFET.Models.Profile;

public class ProfileViewModel
{
    
    public long id { get; set; }
        
    [Required(ErrorMessage = "Вкажіть Ваший вік!")]
    [Range(0, 150, ErrorMessage = "Діапазон вашого віку повинен бути від 0 до 100!")]
    public byte age { get; set; }
        
    [Required(ErrorMessage = "Вкажіть адресу!")]
    [MinLength(5, ErrorMessage = "Адрес повинен мати більше 5-ти символів!")] 
    [MaxLength(200, ErrorMessage = "Адрес повинен мати менше 200-та символів!")]
    public string Address { get; set; }
        
    public string UserName { get; set; }

    public string NewPassword { get; set; }
}