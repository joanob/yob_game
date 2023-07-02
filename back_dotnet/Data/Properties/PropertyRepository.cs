using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class PropertiesRepository : IPropertiesRepository
{
    private readonly AppDbContext _context;

    public PropertiesRepository(AppDbContext context)
    {
        _context = context;
    }

    public Property CreateProperty(Property property)
    {
        PropertyDTO propertyDTO = new PropertyDTO(0, property.UserId, property.ProductionBuildingId);

        try
        {
            _context.Property.Add(propertyDTO);
            property.Id = propertyDTO.Id;
            return property;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Property>> GetAllProperties(int userId)
    {
        var dtos = await _context.Property.Where(p => p.UserId == userId).ToListAsync();

        var properties = new List<Property>();

        foreach (var dto in dtos)
        {
            properties.Add(new Property(dto.Id, dto.UserId, dto.ProductionBuildingId));
        }

        return properties;
    }

    public async Task<List<Property>> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId)
    {
        var dtos = await _context.Property.Where(p => p.UserId == userId && p.ProductionBuildingId == productionBuildingId).ToListAsync();

        var properties = new List<Property>();

        foreach (var dto in dtos)
        {
            properties.Add(new Property(dto.Id, dto.UserId, dto.ProductionBuildingId));
        }

        return properties;
    }
}