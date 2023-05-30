namespace Domain;

public interface IGameDataService
{
    // Resources
    List<Resource> GetAllResources();
    Resource GetResourceById(int Id);

    // Production Buildings
    List<ProductionBuilding> GetAllProductionBuildings();
    ProductionBuilding GetProductionBuildingById(int Id);
    ProductionBuilding GetProductionBuildingWithProcessId(int ProcessId);

    // Production Processes
    List<ProductionProcess> GetAllProductionProcesses();
    ProductionProcess GetProductionProcessById(int Id);
}