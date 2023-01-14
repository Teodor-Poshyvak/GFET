using System.Security.Claims;
using GFET.Enum;
using GFET.Helper;
using GFET.Interface;
using GFET.Models;
using GFET.Models.Profile;
using GFET.Models.User;
using GFET.Response;
using GFET.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace GFET.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IBaseRepository<Profile> _proFileRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly ILogger<AccountService> _logger;

    public AccountService(IBaseRepository<User> userRepository,
        ILogger<AccountService> logger, IBaseRepository<Profile> proFileRepository)
    {
        _userRepository = userRepository;
        _logger = logger;
        _proFileRepository = proFileRepository;
    }

    private ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }

    public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь с таким логином уже есть",
                };
            }

            user = new User()
            {
                Name = model.Name,
                Role = UserRole.User,
                Password = HashPasswordHelper.HashPassowrd(model.Password),
            };

            var profile = new Profile()
            {
                UserId = user.Id,
            };

            await _userRepository.Create(user);
            await _proFileRepository.Create(profile);
            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Объект добавился",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Register]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь не найден"
                };
            }

            if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль или логин"
                };
            }

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<bool>> ChangePassword(ChangePassViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.UserName);
            if (user == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Пользователь не найден"
                };
            }

            user.Password = HashPasswordHelper.HashPassowrd(model.NewPassword);
            await _userRepository.Update(user);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Пароль обновлен"
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}

