namespace PowerPuffBE.Data.Repositories;

using Entities;
using Microsoft.EntityFrameworkCore;

public interface IImageRepository
{
    Task<IEnumerable<ImageEntity>> GetImages();
    Task<ImageEntity> GetImageById(Guid id);

    Task<ImageEntity> GetImageByName(string name);
    Task<Guid> Add(ImageEntity image);
}

public class ImageRepository : IImageRepository
{
    private readonly PowerPuffDbContext _context;

    public ImageRepository(PowerPuffDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ImageEntity>> GetImages()
    {
        return await _context.Image.Where(x => x.ReactorId == null).ToListAsync();
    }

    public async Task<ImageEntity> GetImageById(Guid id)
    {
        return await _context.Image.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<ImageEntity> GetImageByName(string name)
    {
        return await _context.Image.FirstOrDefaultAsync(x=>x.Name.Equals(name));
    }

    public async Task<Guid> Add(ImageEntity image)
    {
        await _context.Image.AddAsync(image);
        await _context.SaveChangesAsync();
        return image.Id;
    }
}