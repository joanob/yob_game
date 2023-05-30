using Newtonsoft.Json;
using Domain;
namespace Services;

public class GameDataService : IGameDataService
{
    private List<Resource> Resources { get; }
    private List<ProductionBuilding> ProductionBuildings { get; }
    private List<ProductionProcess> ProductionProcesses { get; }

    public GameDataService()
    {
        // Read json
        StreamReader sreader = new StreamReader("gamedata.json");
        var rawJSON = sreader.ReadToEnd();
        sreader.Close();

        // Deserialize object works but sets to null (0 for numbers) the values that are not found. Gamedata should always be verified by balancer before using in backend to avoid this kind of problems
        var gamedata = JsonConvert.DeserializeObject<GameData>(rawJSON);

        if (gamedata == null || gamedata.Resources == null || gamedata.ProductionBuildings == null)
        {
            throw new Exception("Gamedata fields are incorrect");
        }

        this.Resources = gamedata.Resources;

        this.ProductionBuildings = gamedata.ProductionBuildings;

        this.ProductionProcesses = getProcessesFromProductionBuildings();
    }

    private List<ProductionProcess> getProcessesFromProductionBuildings()
    {
        var processes = new List<ProductionProcess>();

        foreach (var prodBuilding in ProductionBuildings)
        {
            processes.AddRange(prodBuilding.Processes);
        }

        return processes;
    }

    public List<Resource> GetAllResources()
    {
        return Resources;
    }


    public Resource GetResourceById(int id)
    {
        foreach (var resource in Resources)
        {
            if (resource.Id == id)
            {
                return resource;
            }
        }
        throw new Exception("Resource not found");
    }

    public List<ProductionBuilding> GetAllProductionBuildings()
    {
        return ProductionBuildings;
    }

    public ProductionBuilding GetProductionBuildingById(int id)
    {
        foreach (var prodBuilding in ProductionBuildings)
        {
            if (prodBuilding.Id == id)
            {
                return prodBuilding;
            }
        }
        throw new Exception("Production building not found");
    }

    public ProductionBuilding GetProductionBuildingWithProcessId(int ProcessId)
    {
        foreach (var prodBuilding in ProductionBuildings)
        {
            foreach (var process in prodBuilding.Processes)
            {
                if (process.Id == ProcessId)
                {
                    return prodBuilding;
                }
            }
        }
        throw new Exception("Production building not found");
    }

    public List<ProductionProcess> GetAllProductionProcesses()
    {
        return ProductionProcesses;
    }

    public ProductionProcess GetProductionProcessById(int Id)
    {
        foreach (var process in ProductionProcesses)
        {
            if (process.Id == Id)
            {
                return process;
            }
        }
        throw new Exception("Production process not found");
    }
}