namespace Domain;

public interface IPropertiesService
{
    void BuyProperty(Property property);

    List<Property> GetAllProperties(int userId);

    List<Property> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId);
}