namespace Domain;

public interface IStorageRepository
{
    void CreateResourceStorage(Storage storage);

    Task<List<Storage>> GetAllStorage(int userId);

    Task<Storage> GetResourceStorage(int userId, int resourceId);

    Task AddResourcesToStorage(Storage storage);

    Task SubResourcesToStorage(Storage storage);
}