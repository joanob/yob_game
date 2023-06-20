using Domain;

namespace Data;

public class PropertyDTO : Property
{
    public PropertyDTO(int userId, int productionBuildingId) : base(userId, productionBuildingId) { }
}