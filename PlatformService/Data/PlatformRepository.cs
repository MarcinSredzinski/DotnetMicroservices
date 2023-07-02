using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _dbContext;
    public PlatformRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    public void Create(Platform platform)
    {
        if (platform is not null)
        {
            _dbContext.Platforms.Add(platform);
            SaveChanges();
            return;
        }
       throw new ArgumentNullException(nameof(platform));
    }

    public Platform? Get(int id)
    {
        return _dbContext.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Platform> GetAll()
    {
        return _dbContext.Platforms;
    }

    public bool SaveChanges()
    {
        return (_dbContext.SaveChanges() >= 0);
    }
}
