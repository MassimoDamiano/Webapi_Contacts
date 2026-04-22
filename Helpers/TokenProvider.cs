using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class TokenProvider(IConfiguration configuration)
{
    public string GenerarToken(Usuario usuario)
    {
        string secretKey = configuration["Jwt:Secret"]!;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var descriptot = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Sid,usuario.Id.ToString()),
                new Claim(ClaimTypes.Email,usuario.Email.ToString()),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Departamento","RRHH"),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = cred,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]

        };
        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(descriptot);



        return  token;
    }
}

