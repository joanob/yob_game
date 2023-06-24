namespace Domain;

public interface IMarketService
{
    void BuyResources(int userId, int resourceId, int quantity);

    void SellResources(int userId, int resourceId, int quantity);
}