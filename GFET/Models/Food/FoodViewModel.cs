using System.ComponentModel.DataAnnotations;

namespace GFET.Models.Food;

public class FoodViewModel
{
    public long Id { get; set; }
	
    [Display(Name = "Назва")]
    [Required(ErrorMessage = "Введіть Назву!")]
    [MinLength(2, ErrorMessage = "Мініммально від 2-ч символів!")]
    public string Name { get; set; }
	
    [Display(Name = "Опис")]
    [MinLength(50, ErrorMessage = "Мінімально від 50-ти символів")]
    public string Description { get; set; }
	
    
    [Display(Name = "Вартість")]
    [Required(ErrorMessage = "Введіть вартість!")]
    public decimal Price { get; set; }
    
	
    [Display(Name = "Категорія товару")]
    [Required(ErrorMessage = "Виберіть категорію!")]
    public string CatagoryFood { get; set; }
	
    public IFormFile Avatar { get; set; }
	
    public byte[]? Image { get; set; }
}