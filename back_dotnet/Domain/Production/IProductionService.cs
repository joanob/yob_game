namespace Domain;

public interface IProductionService
{
    void StartProduction(int userId, int productionBuildingId, int productionProcessId, int Quantity);

    List<Production> GetAllProduction(int userId);

    Production GetProduction(int userId, int productionBuildingId);

    void EndProduction(int userId, int productionBuildingId);
}