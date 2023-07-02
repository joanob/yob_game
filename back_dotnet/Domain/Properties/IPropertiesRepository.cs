namespace Domain;

public interface IPropertiesRepository
{
    Property CreateProperty(Property property);

    Task<List<Property>> GetAllProperties(int userId);

    Task<List<Property>> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId);
}