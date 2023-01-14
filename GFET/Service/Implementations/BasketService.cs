using GFET.Enum;
using GFET.Extension;
using GFET.Interface;
using GFET.Models.Food;
using GFET.Models.Property;
using GFET.Models.User;
using GFET.Response;
using GFET.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace GFET.Service.Implementations;

public class BasketService : IBasketService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Food> _foodRepository;

    public BasketService(IBaseRepository<User> userRepository, IBaseRepository<Food> foodRepository)
    {
        _userRepository = userRepository;
        _foodRepository = foodRepository;
    }

    public async Task<IBaseResponse<IEnumerable<PropertyViewModel>>> GetItems(string userName)
    {
        try
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Basket)
                .ThenInclude(x => x.Propertis)
                .FirstOrDefaultAsync(x => x.Name == userName);

            if (user == null)
            {
                return new BaseResponse<IEnumerable<PropertyViewModel>>()
                {
                    Description = "Користувача не знайдено!",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var orders = user.Basket?.Propertis;
            var response = from p in orders
                join c in _foodRepository.GetAll() on p.FoodId equals c.id
                select new PropertyViewModel()
                {
                    Id = p.id,
                    FoodName = c.name,
                    CategoryFood = c.CatagoryFood.GetDisplayName(),
                    Image = c.Avatar
                };

            return new BaseResponse<IEnumerable<PropertyViewModel>>()
            {
                Data = response,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<PropertyViewModel>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    
    
    
public async Task<IBaseResponse<PropertyViewModel>> GetItem(string userName, long id)
{
    try
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Basket)
            .ThenInclude(x => x.Propertis)
            .FirstOrDefaultAsync(x => x.Name == userName);

        if (user == null)
        {
            return new BaseResponse<PropertyViewModel>()
            {
                Description = "Користувача не знайдено!",
                StatusCode = StatusCode.UserNotFound
            };
        }
            
        var orders = user.Basket?.Propertis.Where(x => x.id == id).ToList();
        if (orders == null || orders.Count == 0)
        {
            return new BaseResponse<PropertyViewModel>()
            {
                Description = "Замовлень немає!",
                StatusCode = StatusCode.OrderNotFound
            };
        }

        var response = (from p in orders
            join c in _foodRepository.GetAll() on p.FoodId equals c.id
            select new PropertyViewModel()
            {
                Id = p.id,
                FoodName = c.name,
                CategoryFood = c.CatagoryFood.GetDisplayName(),
                Address = p.Address,
                FirstName = p.FirstName,
                LastName = p.LastName,
                MiddleName = p.MiddleName,
                DateCreate = p.DateCreated.ToLongDateString(),
                Image = c.Avatar
            }).FirstOrDefault();
            
        return new BaseResponse<PropertyViewModel>()
        {
            Data = response,
            StatusCode = StatusCode.OK
        };
    }
    catch (Exception ex)
    {
        return new BaseResponse<PropertyViewModel>()
        {
            Description = ex.Message,
            StatusCode = StatusCode.InternalServerError
        };
    }
}
}