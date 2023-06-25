namespace Domain;

public class Storage
{
    public Storage(int userId, int resourceId, int quantity)
    {
        UserId = userId;
        ResourceId = resourceId;
        Quantity = quantity;
    }

    public int UserId { get; }
    public int ResourceId { get; }
    public int Quantity { get; set; }
}