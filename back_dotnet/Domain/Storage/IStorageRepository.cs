namespace Domain;

public interface IStorageRepository
{
    Task CreateResourceStorage(Storage storage);

    Task<List<Storage>> GetAllStorage(int userId);

    Task<Storage> GetResourceStorage(int userId, int resourceId);

    Task AddResourcesToStorage(Storage storage);

    Task SubResourcesToStorage(Storage storage);
}