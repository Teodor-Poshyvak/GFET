using GFET.Models.Food;
using GFET.Response;

namespace GFET.Service.Interface;

public interface IFoodService
{
    BaseResponse<Dictionary<int, string>> GetTypes();
        
    IBaseResponse<List<Food>> GetFoods();
        
    Task<IBaseResponse<FoodViewModel>> GetFood(long? id);

    Task<BaseResponse<Dictionary<long, string>>> GetFood(string term);

    Task<IBaseResponse<Food>> Create(FoodViewModel food, byte[] imageData);

    Task<IBaseResponse<bool>> DeleteFood(long id);

    Task<IBaseResponse<Food>> Edit(long id, FoodViewModel model);
}