using GFET.Models.Profile;
using GFET.Response;

namespace GFET.Service.Interface;

public interface IProfileService
{
    
    Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);

    Task<BaseResponse<Profile>> Save(ProfileViewModel model);
}