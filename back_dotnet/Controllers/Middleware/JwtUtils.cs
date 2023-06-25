using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Controllers;

public class JwtUtils
{
    public static int? getUserIdFromJWT(string? jwt, string jwtKey)
    {
        if (jwt == null)
        {
            return null;
        }

        return validateToken(jwt, jwtKey);
    }

    public static int? validateToken(string token, string jwtKey)
    {
        if (token == null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                // Check signing key and expiration
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            return userId;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public static string generateToken(User user, string jwtKey)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
        };

        var token = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(15), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}