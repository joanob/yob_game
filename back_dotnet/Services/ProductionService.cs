using Domain;

namespace Services;

public class ProductionService : IProductionService
{
    private readonly IProductionRepository _productionRepository;
    private readonly IStorageService _storageService;
    private readonly IGameDataService _gamedataService;

    public ProductionService(IProductionRepository productionRepository, IStorageService storageService, IGameDataService gameDataService)
    {
        this._productionRepository = productionRepository;
        this._storageService = storageService;
        this._gamedataService = gameDataService;
    }


    public void StartProduction(int userId, int productionBuildingId, int productionProcessId, int Quantity)
    {
        if (Quantity < 1)
        {
            throw new Exception("Quantity is negative or zero");
        }

        var productionBuilding = _gamedataService.GetProductionBuildingById(productionBuildingId);

        var process = productionBuilding.Processes.Where(p => p.Id == productionProcessId).Single();

        if (process == null)
        {
            throw new Exception("Process not in production building");
        }

        var existingProduction = _productionRepository.GetProduction(userId, productionBuildingId);

        if (existingProduction != null)
        {
            throw new Exception("Production building is alreadyin use.");
        }

        foreach (var input in process.Input)
        {
            var requiredQuantity = input.Quantity * Quantity;

            var stored = _storageService.GetResourceStorage(userId, input.ResourceId);

            if (requiredQuantity < stored.Quantity)
            {
                throw new Exception("Not enough resources");
            }
        }

        foreach (var input in process.Input)
        {
            var requiredQuantity = input.Quantity * Quantity;

            _storageService.SubResourcesToStorage(new Storage(userId, input.ResourceId, requiredQuantity));
        }

        long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        long end = start + process.Miliseconds * Quantity;

        _productionRepository.CreateProduction(new Production(userId, productionBuildingId, process.Id, Quantity, start, end));
    }

    public List<Production> GetAllProduction(int userId)
    {
        return _productionRepository.GetAllProduction(userId);
    }

    public Production GetProduction(int userId, int productionBuildingId)
    {
        return _productionRepository.GetProduction(userId, productionBuildingId);
    }

    public void EndProduction(int userId, int productionBuildingId)
    {

        var production = _productionRepository.GetProduction(userId, productionBuildingId);

        if (production == null)
        {
            throw new Exception("Production not started");
        }

        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        if (now < production.End)
        {
            throw new Exception("Production didn't end");
        }

        _productionRepository.EndProduction(userId, productionBuildingId);

        var process = _gamedataService.GetProductionProcessById(production.ProductionProcessId);

        foreach (var output in process.Output)
        {
            var producedQuantity = production.Quantity * output.Quantity;

            _storageService.AddResourcesToStorage(new Storage(userId, output.ResourceId, producedQuantity));
        }
    }

}