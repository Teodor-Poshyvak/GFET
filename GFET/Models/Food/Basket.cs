
using GFET.Enum;

namespace GFET.Models.Food;

public class Basket
{
    public long id { get; set; }

    public User.User? User { get; set; }
        
    public long UserId { get; set; }
        
    public List<Property.Property> Propertis { get; set; }
}