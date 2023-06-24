using Domain;

namespace Data;

public class ProductionRepository : IProductionRepository
{
    private readonly AppDbContext _context;

    public ProductionRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CreateProduction(Production production)
    {
        ProductionDTO productionDTO = new ProductionDTO(production.UserId, production.ProductionBuildingId, production.ProductionProcessId, production.Quantity, production.Start, production.End);

        try
        {
            _context.Production.Add(productionDTO);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public List<Production> GetAllProduction(int userId)
    {
        var dtos = _context.Production.Where(p => p.UserId == userId).ToList();

        var production = new List<Production>();

        foreach (var dto in dtos)
        {
            production.Add(new Production(dto.UserId, dto.ProductionBuildingId, dto.ProductionProcessId, dto.Quantity, dto.Start, dto.End));
        }

        return production;
    }

    public Production GetProduction(int userId, int productionBuildingId)
    {
        var dto = _context.Production.Where(p => p.UserId == userId && p.ProductionBuildingId == productionBuildingId).Single();

        return new Production(dto.UserId, dto.ProductionBuildingId, dto.ProductionProcessId, dto.Quantity, dto.Start, dto.End);
    }

    public void EndProduction(int userId, int productionBuildingId)
    {
        var dto = _context.Production.Where(p => p.UserId == userId && p.ProductionBuildingId == productionBuildingId).Single();

        _context.Remove(dto);
    }
}