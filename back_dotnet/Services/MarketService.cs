using Domain;

namespace Services;

public class MarketService : IMarketService
{
    private readonly IGameDataService _gamedataService;
    private readonly IStorageService _storageService;
    private readonly IUserService _userService;

    public MarketService(IGameDataService gameDataService, IStorageService storageService, IUserService userService)
    {
        _gamedataService = gameDataService;
        _storageService = storageService;
        _userService = userService;
    }

    public void BuyResources(int userId, int resourceId, int quantity)
    {
        var resource = _gamedataService.GetResourceById(resourceId);
        var cost = resource.Price * quantity;

        var user = _userService.GetUserById(userId);
        if (user.CompanyMoney < cost)
        {
            throw new Exception("Not enough money");
        }

        user.CompanyMoney -= (uint)cost;
        _userService.UpdateCompanyMoney(user.Id, (int)user.CompanyMoney);

        _storageService.AddResourcesToStorage(new Storage(user.Id, resourceId, quantity));
    }

    public void SellResources(int userId, int resourceId, int quantity)
    {
        var resource = _gamedataService.GetResourceById(resourceId);
        var cost = resource.Price * quantity;

        var storage = _storageService.GetResourceStorage(userId, resourceId);
        if (storage.Quantity < quantity)
        {
            throw new Exception("Not enough resources");
        }

        var user = _userService.GetUserById(userId);
        user.CompanyMoney += (uint)cost;
        _userService.UpdateCompanyMoney(user.Id, (int)user.CompanyMoney);

        _storageService.SubResourcesToStorage(new Storage(user.Id, resourceId, quantity));
    }
}