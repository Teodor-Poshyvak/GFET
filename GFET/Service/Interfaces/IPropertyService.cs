using GFET.Models.Property;
using GFET.Response;

namespace GFET.Service.Interface;

public interface IPropertyService
{
    Task<IBaseResponse<Property>> Create(CreatePropertyViewModel model);

    Task<IBaseResponse<bool>> Delete(long id);
}