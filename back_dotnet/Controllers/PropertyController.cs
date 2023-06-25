using Domain;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/property")]
public class PropertyController : ControllerBase
{
    private AppDbContext _context;
    private IPropertiesService _propertyService;

    public PropertyController(AppDbContext context, IPropertiesService propertiesService)
    {
        _context = context;
        _propertyService = propertiesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Property>>> GetAllProperties()
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return await _propertyService.GetAllProperties((int)userId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Property>>> GetProperty(int productionBuildingId)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return await _propertyService.GetPropertiesByProductionBuildingId((int)userId, productionBuildingId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult> BuyProperty([FromBody] BuyPropertyCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _propertyService.BuyProperty(new Property(0, (int)userId, cmd.ProdBuildingId));
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}

public class BuyPropertyCmd
{
    public BuyPropertyCmd(int prodBuildingId)
    {
        this.ProdBuildingId = prodBuildingId;
    }
    public int ProdBuildingId { get; set; }
}