using Microsoft.AspNetCore.Mvc;
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _config;
    private IUserService _userService;
    private readonly string jwtKey;

    public UserController(IConfiguration config, IUserService userService)
    {
        _config = config;
        _userService = userService;

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
            await _userService.Signup(cmd.Username, cmd.Password);
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

    [HttpPost("company/name")]
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