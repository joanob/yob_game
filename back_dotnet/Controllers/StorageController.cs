using Domain;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/storage")]
public class StorageController : ControllerBase
{
    private AppDbContext _context;
    private IStorageService _storageService;
    private IMarketService _marketService;

    public StorageController(AppDbContext context, IStorageService storageService, IMarketService marketService)
    {
        _context = context;
        _storageService = storageService;
        _marketService = marketService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Storage>>> GetAllStorage()
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return await _storageService.GetAllStorage((int)userId);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("buy")]
    public async Task<ActionResult> BuyResources([FromBody] BuySellStorageCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _marketService.BuyResources((int)userId, cmd.ResourceId, cmd.Quantity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("sell")]
    public async Task<ActionResult> SellResources([FromBody] BuySellStorageCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _marketService.SellResources((int)userId, cmd.ResourceId, cmd.Quantity);
            await _context.SaveChangesAsync();
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