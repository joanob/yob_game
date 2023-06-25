namespace Domain;

public class Production
{
    public int UserId { get; }
    public int ProductionBuildingId { get; }
    public int ProductionProcessId { get; set; }
    public int Quantity { get; set; }
    public long Start { get; set; }
    public long End { get; set; }

    public Production(int UserId, int ProductionBuildingId, int ProductionProcessId, int Quantity, long Start, long End)
    {
        this.UserId = UserId;
        this.ProductionBuildingId = ProductionBuildingId;
        this.ProductionProcessId = ProductionProcessId;
        this.Quantity = Quantity;
        this.Start = Start;
        this.End = End;
    }
}