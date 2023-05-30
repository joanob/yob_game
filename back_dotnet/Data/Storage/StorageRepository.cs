using Domain;

namespace Data;

public class StorageRepository : IStorageRepository
{
    private readonly AppDbContext _context;

    public StorageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateResourceStorage(Storage storage)
    {
        StorageDTO storageDTO = new StorageDTO(storage.UserId, storage.ResourceId, storage.Quantity);

        try
        {
            _context.Storage.Add(storageDTO);
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public Task<List<Storage>> GetAllStorage(int userId)
    {
        var dtos = _context.Storage.Where(s => s.UserId == userId).ToList();

        var storages = new List<Storage>();

        foreach (var dto in dtos)
        {
            storages.Add(new Storage(dto.UserId, dto.ResourceId, dto.Quantity));
        }

        return Task.FromResult(storages);
    }

    public Task<Storage> GetResourceStorage(int userId, int resourceId)
    {
        var dto = _context.Storage.Where(s => s.UserId == userId && s.ResourceId == resourceId).Single();

        return Task.FromResult(new Storage(dto.UserId, dto.ResourceId, dto.Quantity));
    }

    public async Task AddResourcesToStorage(Storage storage)
    {
        var dto = _context.Storage.Where(s => s.UserId == storage.UserId && s.ResourceId == storage.ResourceId).Single();

        dto.Quantity += storage.Quantity;

        await _context.SaveChangesAsync();
    }

    public async Task SubResourcesToStorage(Storage storage)
    {
        var dto = _context.Storage.Where(s => s.UserId == storage.UserId && s.ResourceId == storage.ResourceId).Single();

        if (dto.Quantity < storage.Quantity)
        {
            throw new Exception("Not enougth resources");
        }

        dto.Quantity -= storage.Quantity;

        await _context.SaveChangesAsync();
    }
}