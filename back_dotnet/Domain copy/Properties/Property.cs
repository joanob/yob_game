namespace Domain;

public class Property
{
    public Property(int Id, int userId, int productionBuildingId)
    {
        this.Id = Id;
        this.UserId = userId;
        this.ProductionBuildingId = productionBuildingId;
    }

    public int Id { get; set; }

    public int UserId { get; }
    public int ProductionBuildingId { get; set; }
}