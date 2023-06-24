using Domain;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/storage")]
public class StorageController : ControllerBase
{
    private AppDbContext _dbContext;
    private IStorageService _storageService;
    private IMarketService _marketService;

    public StorageController(AppDbContext dbContext, IStorageService storageService, IMarketService marketService)
    {
        _dbContext = dbContext;
        _storageService = storageService;
        _marketService = marketService;
    }

    [HttpGet]
    public ActionResult<List<Storage>> GetAllStorage()
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return _storageService.GetAllStorage((int)userId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("buy")]
    public ActionResult BuyResources([FromBody] BuySellStorageCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            _marketService.BuyResources((int)userId, cmd.ResourceId, cmd.Quantity);
            _dbContext.SaveChanges();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("sell")]
    public ActionResult SellResources([FromBody] BuySellStorageCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            _marketService.SellResources((int)userId, cmd.ResourceId, cmd.Quantity);
            _dbContext.SaveChanges();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}

public class BuySellStorageCmd
{
    public BuySellStorageCmd(int ResourceId, int Quantity)
    {
        this.ResourceId = ResourceId;
        this.Quantity = Quantity;
    }

    public int ResourceId { get; set; }
    public int Quantity { get; set; }
}