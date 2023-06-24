namespace Domain;

public interface IProductionRepository
{
    void CreateProduction(Production production);

    List<Production> GetAllProduction(int userId);

    Production GetProduction(int userId, int productionBuildingId);

    void EndProduction(int userId, int productionBuildingId);
}