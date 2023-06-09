namespace Domain;

public interface IProductionService
{
    Task StartProduction(int userId, int productionBuildingId, int productionProcessId, int Quantity);

    Task<List<Production>> GetAllProduction(int userId);

    Task<Production> GetProduction(int userId, int productionBuildingId);

    Task EndProduction(int userId, int productionBuildingId);
}