using GFET.Enum;

namespace GFET.Models.Profile;

public class Profile
{
    public long id { get; set; }
        
    public byte Age { get; set; }
        
    public string Address { get; set; }
        
    public long UserId { get; set; }
        
    public User.User User { get; set; }
}