namespace Domain;

public class Resource
{
    public int Id { get; }
    public string Name { get; }
    public int Price { get; }

    public Resource(int Id, string Name, int Price)
    {
        this.Id = Id;
        this.Name = Name;
        this.Price = Price;
    }
}