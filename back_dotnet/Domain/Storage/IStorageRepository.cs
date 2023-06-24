namespace Domain;

public interface IStorageRepository
{
    void CreateResourceStorage(Storage storage);

    List<Storage> GetAllStorage(int userId);

    Storage GetResourceStorage(int userId, int resourceId);

    void AddResourcesToStorage(Storage storage);

    void SubResourcesToStorage(Storage storage);
}