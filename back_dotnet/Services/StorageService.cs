using Domain;

namespace Services;

public class StorageService : IStorageService
{
    private readonly IStorageRepository _storageRepository;
    private readonly IGameDataService _gamedataService;

    public StorageService(IStorageRepository storageRepository, IGameDataService gameDataService)
    {
        _storageRepository = storageRepository;
        _gamedataService = gameDataService;
    }

    public async Task CreateAllUserStorage(int userId)
    {
        var resources = _gamedataService.GetAllResources();
        var existingStorage = await _storageRepository.GetAllStorage(userId);

        foreach (var resource in resources)
        {
            if (existingStorage.Find(s => s.ResourceId == resource.Id) == null)
            {
                await _storageRepository.CreateResourceStorage(new Storage(userId, resource.Id, 0));
            }
        }

    }

    public async Task<List<Storage>> GetAllStorage(int userId)
    {
        return await _storageRepository.GetAllStorage(userId);
    }

    public async Task<Storage> GetResourceStorage(int userId, int resourceId)
    {
        return await _storageRepository.GetResourceStorage(userId, resourceId);
    }

    public async Task AddResourcesToStorage(Storage storage)
    {
        await _storageRepository.AddResourcesToStorage(storage);
    }

    public async Task SubResourcesToStorage(Storage storage)
    {
        var stored = await _storageRepository.GetResourceStorage(storage.UserId, storage.ResourceId);

        if (stored.Quantity < storage.Quantity)
        {
            throw new Exception("Not enough resources in storage");
        }

        await _storageRepository.SubResourcesToStorage(storage);
    }
}