using Domain;

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

    public List<Storage> GetAllStorage(int userId)
    {
        var dtos = _context.Storage.Where(s => s.UserId == userId).ToList();

        var storages = new List<Storage>();

        foreach (var dto in dtos)
        {
            storages.Add(new Storage(dto.UserId, dto.ResourceId, dto.Quantity));
        }

        return storages;
    }

    public Storage GetResourceStorage(int userId, int resourceId)
    {
        var dto = _context.Storage.Where(s => s.UserId == userId && s.ResourceId == resourceId).Single();

        return new Storage(dto.UserId, dto.ResourceId, dto.Quantity);
    }

    public void AddResourcesToStorage(Storage storage)
    {
        var dto = _context.Storage.Where(s => s.UserId == storage.UserId && s.ResourceId == storage.ResourceId).Single();

        dto.Quantity += storage.Quantity;
    }

    public void SubResourcesToStorage(Storage storage)
    {
        var dto = _context.Storage.Where(s => s.UserId == storage.UserId && s.ResourceId == storage.ResourceId).Single();

        if (dto.Quantity < storage.Quantity)
        {
            throw new Exception("Not enougth resources");
        }

        dto.Quantity -= storage.Quantity;
    }
}