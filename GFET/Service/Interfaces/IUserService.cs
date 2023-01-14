using GFET.Enum;
using GFET.Models.User;
using GFET.Response;

namespace GFET.Service.Interface;

public interface IUserService
{
    Task<IBaseResponse<User>> Create(UserViewModel model);
        
    BaseResponse<Dictionary<int, string>> GetRoles();
        
    Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        
    Task<IBaseResponse<bool>> DeleteUser(long id);
}