using Domain;

namespace Data;

public class ProductionDTO : Production
{
    public ProductionDTO(int UserId, int ProductionBuildingId, int ProductionProcessId, int Quantity, long Start, long End) : base(UserId, ProductionBuildingId, ProductionProcessId, Quantity, Start, End) { }
}