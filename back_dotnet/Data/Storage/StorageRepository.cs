using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class StorageRepository : IStorageRepository
{
    private readonly AppDbContext _context;

    public StorageRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CreateResourceStorage(Storage storage)
    {
        StorageDTO storageDTO = new StorageDTO(storage.UserId, storage.ResourceId, storage.Quantity);

        try
        {
            _context.Storage.Add(storageDTO);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Storage>> GetAllStorage(int userId)
    {
        var dtos = await _context.Storage.Where(s => s.UserId == userId).ToListAsync();

        var storages = new List<Storage>();

        foreach (var dto in dtos)
        {
            storages.Add(new Storage(dto.UserId, dto.ResourceId, dto.Quantity));
        }

        return storages;
    }

    public async Task<Storage> GetResourceStorage(int userId, int resourceId)
    {
        var dto = await _context.Storage.Where(s => s.UserId == userId && s.ResourceId == resourceId).SingleAsync();

        return new Storage(dto.UserId, dto.ResourceId, dto.Quantity);
    }

    public async Task AddResourcesToStorage(Storage storage)
    {
        var dto = await _context.Storage.Where(s => s.UserId == storage.UserId && s.ResourceId == storage.ResourceId).SingleAsync();

        dto.Quantity += storage.Quantity;
    }

    public async Task SubResourcesToStorage(Storage storage)
    {
        var dto = await _context.Storage.Where(s => s.UserId == storage.UserId && s.ResourceId == storage.ResourceId).SingleAsync();

        if (dto.Quantity < storage.Quantity)
        {
            throw new Exception("Not enougth resources");
        }

        dto.Quantity -= storage.Quantity;
    }
}