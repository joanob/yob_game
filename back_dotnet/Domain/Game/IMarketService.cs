namespace Domain;

public interface IMarketService
{
    Task BuyResources(int userId, int resourceId, int quantity);

    Task SellResources(int userId, int resourceId, int quantity);
}