using GFET.Enum;
using GFET.Interface;
using GFET.Models.Property;
using GFET.Models.User;
using GFET.Response;
using GFET.Service.Interface;
using Microsoft.EntityFrameworkCore;


namespace GFET.Service.Implementations;

public class PropertyService : IPropertyService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Property> _propertyRepository;

    public PropertyService(IBaseRepository<User> userRepository, IBaseRepository<Property> propertyRepository)
    {
        _userRepository = userRepository;
        _propertyRepository = propertyRepository;
    }


    public async Task<IBaseResponse<Property>> Create(CreatePropertyViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Basket)
                .FirstOrDefaultAsync(x => x.Name == model.Login);
            if (user == null)
            {
                return new BaseResponse<Property>()
                {
                    Description = "Користувача не знайдено!",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var property = new Property()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Address = model.Address,
                BasketId = user.Basket.id,
                FoodId = model.FoodId
            };

            await _propertyRepository.Create(property);

            return new BaseResponse<Property>()
            {
                Description = "Замовлення прийняте!",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Property>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }



    public async Task<IBaseResponse<bool>> Delete(long id)
    {
        try
        {
            var property = _propertyRepository.GetAll()
                .Select(x => x.Basket.Propertis.FirstOrDefault(y => y.id == id))
                .FirstOrDefault();

            if (property == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OrderNotFound,
                    Description = "Замовлення не знайдено!"
                };
            }

            await _propertyRepository.Delete(property);
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.OK,
                Description = "Замовлення видалено!"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}