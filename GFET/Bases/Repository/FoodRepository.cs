using GFET.Base;
using GFET.Interface;
using GFET.Models.Food;
using GFET.Response;
using GFET.Service.Interface;

namespace GFET.Repositories;

public class FoodRepository : IBaseRepository<Food>
{
    private readonly ApplicationDbContext _db;

    public FoodRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task Create(Food entity)
    {
        await _db.Foods.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public IQueryable<Food> GetAll()
    {
        return _db.Foods;
    }

    public async Task Delete(Food entity)
    {
        _db.Foods.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Food> Update(Food entity)
    {
        _db.Foods.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}