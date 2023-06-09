namespace Domain;

public interface IProductionRepository
{
    Task CreateProduction(Production production);

    Task<List<Production>> GetAllProduction(int userId);

    Task<Production> GetProduction(int userId, int productionBuildingId);

    Task EndProduction(int userId, int productionBuildingId);
}