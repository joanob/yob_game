namespace Domain;

public interface IPropertiesRepository
{
    void CreateProperty(Property property);

    List<Property> GetAllProperties(int userId);

    List<Property> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId);
}