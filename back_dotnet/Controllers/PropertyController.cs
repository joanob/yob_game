using Domain;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/property")]
public class PropertyController : ControllerBase
{
    private AppDbContext _dbContext;
    private IPropertiesService _propertyService;

    public PropertyController(AppDbContext dbContext, IPropertiesService propertiesService)
    {
        _dbContext = dbContext;
        _propertyService = propertiesService;
    }

    [HttpGet]
    public ActionResult<List<Property>> GetAllProperties()
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return _propertyService.GetAllProperties((int)userId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public ActionResult<List<Property>> GetProperty(int productionBuildingId)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return _propertyService.GetPropertiesByProductionBuildingId((int)userId, productionBuildingId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public ActionResult BuyProperty([FromBody] BuyPropertyCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            _propertyService.BuyProperty(new Property(0, (int)userId, cmd.ProdBuildingId));
            _dbContext.SaveChanges();
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