using Domain;

namespace Data;

public class StorageDTO : Storage
{
    public StorageDTO(int UserId, int ResourceId, int Quantity) : base(UserId, ResourceId, Quantity) { }
}