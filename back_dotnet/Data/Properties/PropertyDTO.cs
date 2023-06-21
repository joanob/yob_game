using Domain;

namespace Data;

public class PropertyDTO : Property
{
    public PropertyDTO(int Id, int userId, int productionBuildingId) : base(Id, userId, productionBuildingId) { }
}