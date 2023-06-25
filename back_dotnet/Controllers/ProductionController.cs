using Domain;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/production")]
public class ProductionController : ControllerBase
{
    private AppDbContext _context;
    private IProductionService _productionService;

    public ProductionController(AppDbContext context, IProductionService productionService)
    {
        _context = context;
        this._productionService = productionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Production>>> GetAllProduction()
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return await _productionService.GetAllProduction((int)userId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Production>> GetProduction(int productionBuildingId)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return await _productionService.GetProduction((int)userId, productionBuildingId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> StartProduction(int productionBuildingId, [FromBody] StartProductionCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _productionService.StartProduction((int)userId, productionBuildingId, cmd.ProcessId, cmd.Quantity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> EndProduction(int productionBuildingId)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _productionService.EndProduction((int)userId, productionBuildingId);
            await _context.SaveChangesAsync();
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