using GFET.Base;
using GFET.Enum;
using GFET.Interface;
using GFET.Models.Profile;
using GFET.Response;

namespace GFET.Repositories;

public class ProfileRepositories : IBaseRepository<Profile>
{
    private readonly ApplicationDbContext _dbContext;

    public ProfileRepositories(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Profile entity)
    {
        await _dbContext.Profiles.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<Profile> GetAll()
    {
       
        return _dbContext.Profiles;
    }

    public async Task Delete(Profile entity)
    {
        _dbContext.Profiles.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Profile> Update(Profile entity)
    {
        _dbContext.Profiles.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
}