using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/storage")]
public class StorageController : ControllerBase
{
    private IStorageService _storageService;
    private IMarketService _marketService;

    public StorageController(IStorageService storageService, IMarketService marketService)
    {
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
    public async Task<ActionResult> BuyResources(BuySellStorageCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _marketService.BuyResources((int)userId, cmd.ResourceId, cmd.Quantity);
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("sell")]
    public async Task<ActionResult> SellResources(BuySellStorageCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _marketService.SellResources((int)userId, cmd.ResourceId, cmd.Quantity);
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