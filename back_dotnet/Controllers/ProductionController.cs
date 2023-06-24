using Domain;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/production")]
public class ProductionController : ControllerBase
{
    private AppDbContext _dbContext;
    private IProductionService _productionService;

    public ProductionController(AppDbContext dbContext, IProductionService productionService)
    {
        _dbContext = dbContext;
        this._productionService = productionService;
    }

    [HttpGet]
    public ActionResult<List<Production>> GetAllProduction()
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return _productionService.GetAllProduction((int)userId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Production> GetProduction(int productionBuildingId)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return _productionService.GetProduction((int)userId, productionBuildingId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("{id}")]
    public ActionResult StartProduction(int productionBuildingId, [FromBody] StartProductionCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            _productionService.StartProduction((int)userId, productionBuildingId, cmd.ProcessId, cmd.Quantity);
            _dbContext.SaveChanges();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteResource(int productionBuildingId)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            _productionService.EndProduction((int)userId, productionBuildingId);
            _dbContext.SaveChanges();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}

public class StartProductionCmd
{
    public StartProductionCmd(int processId, int quantity)
    {
        this.ProcessId = processId;
        this.Quantity = quantity;
    }

    public int ProcessId { get; set; }
    public int Quantity { get; set; }
}