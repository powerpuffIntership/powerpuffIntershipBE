namespace PowerPuffBE.Data.Repositories;

using Entities;
using Microsoft.EntityFrameworkCore;

public interface IReactorRepository
{
    Task<IEnumerable<ReactorEntity>> GetAllReactors(bool extended = false);
    Task<ReactorEntity> GetReactorExtendedById(Guid id);
    Task<IEnumerable<ReactorEntity>> GetReactorImageList();
    Task Update(ReactorEntity reactor);
}
public class ReactorRepository : IReactorRepository
{
    private readonly PowerPuffDbContext _context;

    public ReactorRepository(PowerPuffDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReactorEntity>> GetAllReactors(bool extended = false)
    {
        var reactors = _context.Reactors.AsQueryable();
        if (extended)
        {
            reactors = _context.Reactors.Include(r => r.ProductionChecks).AsQueryable();
        }

        return await reactors.ToListAsync();
    }
    
    public async Task<IEnumerable<ReactorEntity>> GetReactorImageList()
    {
        return await _context.Reactors.Where(r => r.ImageId != Guid.Empty).ToListAsync();
    }

    public async Task<ReactorEntity> GetReactorExtendedById(Guid id)
    {
        return await _context.Reactors
            .Include(r => r.ProductionChecks)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task Update(ReactorEntity reactor)
    {
        _context.Reactors.Update(reactor);
        await _context.SaveChangesAsync();
    }
}