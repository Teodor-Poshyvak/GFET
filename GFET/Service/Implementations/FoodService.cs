using GFET.Enum;
using GFET.Extension;
using GFET.Interface;
using GFET.Models.Food;
using GFET.Response;
using GFET.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace GFET.Service.Implementations;

public class FoodService : IFoodService
{
    private readonly IBaseRepository<Food> _foodRepository;

    public FoodService(IBaseRepository<Food> foodRepository)
    {
        _foodRepository = foodRepository;
    }
    
    
    public BaseResponse<Dictionary<int, string>> GetTypes()
    {
        try
        {
            var types = ((CategoryFood[]) System.Enum.GetValues(typeof(CategoryFood)))
                .ToDictionary(k => (int) k, t => t.GetDisplayName());

            return new BaseResponse<Dictionary<int, string>>()
            {
                Data = types,
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

    public IBaseResponse<List<Food>> GetFoods()
    {
        try
        {
            var foods = _foodRepository.GetAll().ToList();
            if (!foods.Any())
            {
                return new BaseResponse<List<Food>>()
                {
                    Description = "Знайдено 0 елементів!",
                    StatusCode = StatusCode.OK
                };
            }
                
            return new BaseResponse<List<Food>>()
            {
                Data = foods,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Food>>()
            {
                Description = $"[GetFoods] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }


    public async Task<IBaseResponse<FoodViewModel>> GetFood(long? id)
 
    {
        try
        {
            var food = await _foodRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
            if (food == null)
            {
                return new BaseResponse<FoodViewModel>()
                {
                    Description = "Користувача не знайдено!",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var data = new FoodViewModel()
            {
                Description = food.description,
                Name = food.name,
                Price = food.price,
                CatagoryFood = food.CatagoryFood.GetDisplayName(),
                Image = food.Avatar,
            };

            return new BaseResponse<FoodViewModel>()
            {
                StatusCode = StatusCode.OK,
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<FoodViewModel>()
            {
                Description = $"[GetFood] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<Dictionary<long, string>>> GetFood(string term)
    {
        var baseResponse = new BaseResponse<Dictionary<long, string>>();
        try
        {
            var foods = await _foodRepository.GetAll()
                .Select(x => new FoodViewModel()
                {
                    Id = x.id,
                    Name = x.name,
                    Description = x.description,
                    Price = x.price,
                    CatagoryFood = x.CatagoryFood.GetDisplayName()
                })
                .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                .ToDictionaryAsync(x => x.Id, t => t.Name);

            baseResponse.Data = foods;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Dictionary<long, string>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Food>> Create(FoodViewModel model, byte[] imageData)
    {
        try
        {
            var food = new Food()
            {    
                name = model.Name,
                description = model.Description,
                CatagoryFood = (CategoryFood)Convert.ToInt32(model.CatagoryFood),
                price = model.Price,
                Avatar = imageData
            }; 
            await _foodRepository.Create(food);

            return new BaseResponse<Food>()
            {
                StatusCode = StatusCode.OK,
                Data = food
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Food>()
            {
                Description = $"[Create] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }


    public async Task<IBaseResponse<bool>> DeleteFood(long id)
    {
        try
        {
            var food = await _foodRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
            if (food == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "User not found",
                    StatusCode = StatusCode.UserNotFound,
                    Data = false
                };
            }

            await _foodRepository.Delete(food);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteFood] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }


    public async Task<IBaseResponse<Food>> Edit(long id, FoodViewModel model)
    {
        try
        {
            var food = await _foodRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
            if (food == null)
            {
                return new BaseResponse<Food>()
                {
                    Description = "Car not found",
                    StatusCode = StatusCode.FoodNotFound
                };
            }

            food.description = model.Description;
            food.price = model.Price;
            food.name = model.Name;

            await _foodRepository.Update(food);


            return new BaseResponse<Food>()
            {
                Data = food,
                StatusCode = StatusCode.OK,
            };
            // TypeFood!
        }
        catch (Exception ex)
        {
            return new BaseResponse<Food>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}