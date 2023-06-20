using Domain;

namespace Data;

public class PropertiesRepository : IPropertiesRepository
{
    private readonly AppDbContext _context;

    public PropertiesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateProperty(Property property)
    {
        PropertyDTO propertyDTO = new PropertyDTO(property.UserId, property.ProductionBuildingId);

        try
        {
            _context.Property.Add(propertyDTO);
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }

    }

    public Task<List<Property>> GetAllProperties(int userId)
    {
        var dtos = _context.Property.Where(p => p.UserId == userId).ToList();

        var properties = new List<Property>();

        foreach (var dto in dtos)
        {
            properties.Add(new Property(dto.UserId, dto.ProductionBuildingId));
        }

        return Task.FromResult(properties);
    }

    public Task<List<Property>> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId)
    {
        var dtos = _context.Property.Where(p => p.UserId == userId && p.ProductionBuildingId == productionBuildingId).ToList();

        var properties = new List<Property>();

        foreach (var dto in dtos)
        {
            properties.Add(new Property(dto.UserId, dto.ProductionBuildingId));
        }

        return Task.FromResult(properties);
    }
}