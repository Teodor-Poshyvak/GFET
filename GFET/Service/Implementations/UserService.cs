using GFET.Enum;
using GFET.Extension;
using GFET.Helper;
using GFET.Interface;
using GFET.Models.Profile;
using GFET.Models.User;
using GFET.Response;
using GFET.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace GFET.Service.Implementations;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IBaseRepository<Profile> _proFileRepository;
    private readonly IBaseRepository<User> _userRepository;
    
    public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository,
        IBaseRepository<Profile> proFileRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _proFileRepository = proFileRepository;
    }

    public async Task<IBaseResponse<User>> Create(UserViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
            if (user != null)
            {
                return new BaseResponse<User>()
                {
                    Description = "Користувач з таким логіном вже існує!",
                    StatusCode = StatusCode.UserAlreadyExists
                };
            }
            user = new User()
            {
                Name = model.Name,
                Role = System.Enum.Parse<UserRole>(model.Role),
                Password = HashPasswordHelper.HashPassowrd(model.Password),
            };
                
            await _userRepository.Create(user);
                
            var profile = new Profile()
            {
                Address = string.Empty,
                Age = 0,
                UserId = user.Id,
            };
                
            await _proFileRepository.Create(profile);
                
            return new BaseResponse<User>()
            {
                Data = user,
                Description = "Користувача додано!",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
            return new BaseResponse<User>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Серверна помилка: {ex.Message}"
            };
        }
    }
    
      public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((UserRole[]) System.Enum.GetValues(typeof(UserRole)))
                    .ToDictionary(k => (int) k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                _logger.LogInformation($"[UserService.GetUsers] отримано елементів {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Серверна помилка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] користувача видалено!");
                
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Серверна помилка: {ex.Message}"
                };
            }
        }
    
}