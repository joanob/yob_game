namespace Domain;

public class Property
{
    public Property(int userId, int productionBuildingId)
    {
        this.UserId = userId;
        this.ProductionBuildingId = productionBuildingId;
    }

    public int UserId { get; }
    public int ProductionBuildingId { get; }
}