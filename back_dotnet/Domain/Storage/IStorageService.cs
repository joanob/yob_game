namespace Domain;

public interface IStorageService
{
    Task CreateAllUserStorage(int userId);

    Task<List<Storage>> GetAllStorage(int userId);

    Task<Storage> GetResourceStorage(int userId, int resourceId);

    Task AddResourcesToStorage(Storage storage);

    Task SubResourcesToStorage(Storage storage);
}