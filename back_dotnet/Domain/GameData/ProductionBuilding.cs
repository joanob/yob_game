namespace Domain;

public class ProductionBuilding
{
    public int Id { get; }
    public string Name { get; }
    public int BuildCost { get; }
    public List<ProductionProcess> Processes { get; }

    public ProductionBuilding(int Id, string Name, int BuildCost, List<ProductionProcess> Processes)
    {
        this.Id = Id;
        this.Name = Name;
        this.BuildCost = BuildCost;
        this.Processes = Processes;
    }
}

public class ProductionProcess
{
    public int Id { get; }
    public string Name { get; }
    public List<ProductionProcessResource> Input { get; }
    public int Miliseconds { get; }
    public List<ProductionProcessResource> Output { get; }

    public ProductionProcess(int Id, string Name, List<ProductionProcessResource> Input,
     int Miliseconds,
     List<ProductionProcessResource> Output)
    {
        this.Id = Id;
        this.Name = Name;
        this.Input = Input;
        this.Miliseconds = Miliseconds;
        this.Output = Output;
    }
}

public class ProductionProcessResource
{
    public int ResourceId { get; }
    public int Quantity { get; }

    public ProductionProcessResource(int ResourceId, int Quantity)
    {
        this.ResourceId = ResourceId;
        this.Quantity = Quantity;
    }
}