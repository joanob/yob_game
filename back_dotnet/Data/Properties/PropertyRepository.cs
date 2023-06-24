using Domain;

namespace Data;

public class PropertiesRepository : IPropertiesRepository
{
    private readonly AppDbContext _context;

    public PropertiesRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CreateProperty(Property property)
    {
        PropertyDTO propertyDTO = new PropertyDTO(0, property.UserId, property.ProductionBuildingId);

        try
        {
            _context.Property.Add(propertyDTO);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public List<Property> GetAllProperties(int userId)
    {
        var dtos = _context.Property.Where(p => p.UserId == userId).ToList();

        var properties = new List<Property>();

        foreach (var dto in dtos)
        {
            properties.Add(new Property(dto.Id, dto.UserId, dto.ProductionBuildingId));
        }

        return properties;
    }

    public List<Property> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId)
    {
        var dtos = _context.Property.Where(p => p.UserId == userId && p.ProductionBuildingId == productionBuildingId).ToList();

        var properties = new List<Property>();

        foreach (var dto in dtos)
        {
            properties.Add(new Property(dto.Id, dto.UserId, dto.ProductionBuildingId));
        }

        return properties;
    }
}