using GFET.Models.Property;
using GFET.Response;

namespace GFET.Service.Interface;

public interface IBasketService
{
    
    Task<IBaseResponse<IEnumerable<PropertyViewModel>>> GetItems(string userName);

    Task<IBaseResponse<PropertyViewModel>> GetItem(string userName, long id);
}