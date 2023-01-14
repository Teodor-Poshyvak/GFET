using GFET.Enum;

namespace GFET.Models.Food;

public class Food
{
    public int id { get; set; }
    
    public string name { get; set; }
    
    public string description { get; set; }
    
    public decimal price { get; set; }
    
    public DateTime dataopen { get; set; }

    public CategoryFood CatagoryFood { get; set;}
    
    public byte[]? Avatar { get; set; }
}