using GFET.Enum;
using GFET.Interface;
using GFET.Models.Profile;
using GFET.Response;
using GFET.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace GFET.Service.Implementations;

  public class ProfileService : IProfileService
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly IBaseRepository<Profile> _profileRepository;

        public ProfileService(IBaseRepository<Profile> profileRepository,
            ILogger<ProfileService> logger)
        {
            _profileRepository = profileRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<ProfileViewModel>> GetProfile(string userName)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .Select(x => new ProfileViewModel()
                    {
                        id = x.id,
                        Address = x.Address,
                        age = x.Age,
                        UserName = x.User.Name
                    })
                    .FirstOrDefaultAsync(x => x.UserName == userName);

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ProfileService.GetProfile] error: {ex.Message}");
                return new BaseResponse<ProfileViewModel>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<Profile>> Save(ProfileViewModel model)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.id == model.id);

                profile.Address = model.Address;
                profile.Age = model.age;

                await _profileRepository.Update(profile);

                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    Description = "Дані оновлено!",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ProfileService.Save] error: {ex.Message}");
                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутрішня помилка!: {ex.Message}"
                };
            } 
        }
        
    }
