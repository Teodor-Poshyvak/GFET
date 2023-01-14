using System.Security.Claims;
using GFET.Models;
using GFET.Response;

namespace GFET.Service.Interface;

public interface IAccountService
{
    
    Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

    Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

    Task<BaseResponse<bool>> ChangePassword(ChangePassViewModel model);
}