namespace Domain;

public interface IPropertiesService
{
    Task<Property> BuyProperty(Property property);

    Task<List<Property>> GetAllProperties(int userId);

    Task<List<Property>> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId);
}