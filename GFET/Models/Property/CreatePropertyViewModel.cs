using System.ComponentModel.DataAnnotations;

namespace GFET.Models.Property;

public class CreatePropertyViewModel
{
    public int id { get; set; }
        
    [Display(Name = "Кількість")]
    [Range(1, 10, ErrorMessage = "Кількість повинна бути від 1 до 10!")]
    public int Quantity { get; set; }

    [Display(Name = "Дата створення")]
    public DateTime DateCreated { get; set; }
        
    [Display(Name = "Адрес")]
    [Required(ErrorMessage = "Введіть адрес")]
    [MinLength(5, ErrorMessage = "Адрес повинен мати більше 5-ти символів!")] 
    [MaxLength(200, ErrorMessage = "Адрес повинен мати менше 100-а символів!")]
    public string Address { get; set; }
        
    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Введіть ім'я")]
    [MaxLength(20, ErrorMessage = "Ім'я повинне мати менше 20-ти символів!")]
    [MinLength(3, ErrorMessage = "Ім'я повинне мати більше 3-х символів!")]
    public string FirstName { get; set; }
        
    [Display(Name = "Прізвище")]
    [MaxLength(50, ErrorMessage = "Прізвище повинно мати менше 50-ти символів")]
    [MinLength(2, ErrorMessage = "Прізвище повинне мати більше 2-х символів!")]
    public string LastName { get; set; }
        
    [Display(Name = "По Батькові")]
    [MaxLength(50, ErrorMessage = "По Батькові повинне мати менше 50-ти символів!")]
    [MinLength(2, ErrorMessage = "По Батькові повинно мати більше 3-х символів!")]
    public string MiddleName { get; set; }
        
    public long FoodId { get; set; }
        
    public string Login { get; set; }
}