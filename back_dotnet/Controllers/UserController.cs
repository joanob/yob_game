using Domain;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _config;
    private AppDbContext _context;
    private IUserService _userService;
    private IStorageService _storageService;
    private readonly string jwtKey;

    public UserController(IConfiguration config, AppDbContext context, IUserService userService, IStorageService storageService)
    {
        _config = config;
        _context = context;
        _userService = userService;
        _storageService = storageService;

        var configJwtKey = _config["Jwt:Key"];
        if (configJwtKey == null)
        {
            throw new Exception("jwt key");
        }
        jwtKey = configJwtKey;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<ActionResult> Signup([FromBody] UserSignupLoginCmd cmd)
    {
        try
        {
            _userService.Signup(cmd.Username, cmd.Password);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<User>> Login([FromBody] UserSignupLoginCmd cmd)
    {
        try
        {
            var user = await _userService.Login(cmd.Username, cmd.Password);

            await _storageService.CreateAllUserStorage(user.Id);

            await _context.SaveChangesAsync();

            var token = JwtUtils.generateToken(user, jwtKey);

            // Set httponly cookie with jwt and a visible cookie with true
            Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
            Response.Cookies.Append("X-Session-Started", "true", new CookieOptions() { HttpOnly = false, SameSite = SameSiteMode.None, Secure = true });

            return user;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("session")]
    public async Task<ActionResult<User>> GetLoggedInUser()
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            return await _userService.GetUserById((int)userId);

        }
        catch (System.Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut("company/name")]
    public async Task<ActionResult> UpdateCompanyName([FromBody] UpdateCompanyNameCmd cmd)
    {
        var userId = HttpContext.Items["UserId"];

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _userService.UpdateCompanyName((int)userId, cmd.CompanyName);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception)
        {
            return StatusCode(400);
        }
    }
}

public class UserSignupLoginCmd
{
    public UserSignupLoginCmd(string Username, string Password)
    {
        this.Username = Username;
        this.Password = Password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}

public class UpdateCompanyNameCmd
{
    public UpdateCompanyNameCmd(string CompanyName)
    {
        this.CompanyName = CompanyName;
    }

    public string CompanyName { get; set; }
}