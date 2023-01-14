

using GFET.Enum;
using GFET.Models.Food;

namespace GFET.Models.User;

public class User
{
    public long Id { get; set; }
        
    public string Password { get; set; }

    public string Name { get; set; }

    public UserRole Role { get; set; }
        
    public Profile.Profile Profile { get; set; }
        
    public Basket Basket { get; set; }
}