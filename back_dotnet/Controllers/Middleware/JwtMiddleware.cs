namespace Controllers;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    private readonly string jwtKey;

    public JwtMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;

        var configJwtKey = _config["Jwt:Key"];
        if (configJwtKey == null)
        {
            throw new Exception("jwt key missing");
        }
        jwtKey = configJwtKey;
    }

    public async Task Invoke(HttpContext context)
    {
        var jwt = context.Request.Cookies["X-Access-Token"];
        var userId = JwtUtils.getUserIdFromJWT(jwt, jwtKey);
        if (userId != null)
        {
            context.Items["UserId"] = userId;
        }
        await _next(context);
    }
}