namespace Domain;

public interface IStorageService
{
    void CreateAllUserStorage(int userId);

    List<Storage> GetAllStorage(int userId);

    Storage GetResourceStorage(int userId, int resourceId);

    void AddResourcesToStorage(Storage storage);

    void SubResourcesToStorage(Storage storage);
}