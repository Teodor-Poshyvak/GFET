using GFET.Interface;
using GFET.Models.Food;
using GFET.Models.Profile;
using GFET.Models.Property;
using GFET.Models.User;
using GFET.Repositories;
using GFET.Service.Implementations;
using GFET.Service.Interface;

namespace GFET;

public static class Initial
{
    
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Food>, FoodRepository>();
        services.AddScoped<IBaseRepository<User>, UserRepository>();
        services.AddScoped<IBaseRepository<Profile>, ProfileRepositories>();
        services.AddScoped<IBaseRepository<Basket>, BasketRepository>();
        services.AddScoped<IBaseRepository<Property>, PropertyRepository>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IFoodService, FoodService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IPropertyService, PropertyService>();
    }
}