using GFET.Base;
using GFET.Interface;
using GFET.Models.Property;

namespace GFET.Repositories;

public class PropertyRepository : IBaseRepository<Property>
{
    private readonly ApplicationDbContext _db;

    public PropertyRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task Create(Property entity)
    {
        await _db.Properties.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public IQueryable<Property> GetAll()
    {
        return _db.Properties;
    }

    public async Task Delete(Property entity)
    {
        _db.Properties.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Property> Update(Property entity)
    {
        _db.Properties.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}