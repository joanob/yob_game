using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/gamedata")]
public class GameDataController : ControllerBase
{
    private IGameDataService _gamedataService;

    public GameDataController(IGameDataService gameDataService)
    {
        _gamedataService = gameDataService;
    }

    [HttpGet("resources")]
    public ActionResult<List<Resource>> GetAllResources()
    {
        return _gamedataService.GetAllResources();
    }

    [HttpGet("production-buildings")]
    public ActionResult<List<ProductionBuilding>> GetAllProductionBuilidings()
    {
        return _gamedataService.GetAllProductionBuildings();
    }
}