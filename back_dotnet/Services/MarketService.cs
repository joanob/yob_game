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

    public async Task BuyResources(int userId, int resourceId, int quantity)
    {
        var resource = _gamedataService.GetResourceById(resourceId);
        var cost = resource.Price * quantity;

        var user = await _userService.GetUserById(userId);
        if (user.CompanyMoney < cost)
        {
            throw new Exception("Not enough money");
        }

        user.CompanyMoney -= (uint)cost;
        await _userService.UpdateCompanyMoney(user.Id, (int)user.CompanyMoney);

        await _storageService.AddResourcesToStorage(new Storage(user.Id, resourceId, quantity));
    }

    public async Task SellResources(int userId, int resourceId, int quantity)
    {
        var resource = _gamedataService.GetResourceById(resourceId);
        var cost = resource.Price * quantity;

        var storage = await _storageService.GetResourceStorage(userId, resourceId);
        if (storage.Quantity < quantity)
        {
            throw new Exception("Not enough resources");
        }

        var user = await _userService.GetUserById(userId);
        user.CompanyMoney += (uint)cost;
        await _userService.UpdateCompanyMoney(user.Id, (int)user.CompanyMoney);

        await _storageService.SubResourcesToStorage(new Storage(user.Id, resourceId, quantity));
    }
}