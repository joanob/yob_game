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

    public void CreateAllUserStorage(int userId)
    {
        var resources = _gamedataService.GetAllResources();
        var existingStorage = _storageRepository.GetAllStorage(userId);

        foreach (var resource in resources)
        {
            if (existingStorage.Find(s => s.ResourceId == resource.Id) == null)
            {
                _storageRepository.CreateResourceStorage(new Storage(userId, resource.Id, 0));
            }
        }

    }

    public List<Storage> GetAllStorage(int userId)
    {
        return _storageRepository.GetAllStorage(userId);
    }

    public Storage GetResourceStorage(int userId, int resourceId)
    {
        return _storageRepository.GetResourceStorage(userId, resourceId);
    }

    public void AddResourcesToStorage(Storage storage)
    {
        _storageRepository.AddResourcesToStorage(storage);
    }

    public void SubResourcesToStorage(Storage storage)
    {
        var stored = _storageRepository.GetResourceStorage(storage.UserId, storage.ResourceId);

        if (stored.Quantity < storage.Quantity)
        {
            throw new Exception("Not enough resources in storage");
        }

        _storageRepository.SubResourcesToStorage(storage);
    }
}