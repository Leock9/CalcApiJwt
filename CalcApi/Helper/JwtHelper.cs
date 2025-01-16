using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CalcApi.Helper;

public static class JwtHelper
{
    private static readonly string _secretKey = "YourStrongSecretKeyHereWith256BitsLength";
    private static readonly string _validIssuer = "CalcApi";
    private static readonly string _validAudience = "CalcApi";

    public static bool ValidClaims(string token, string roleEndpoint)
    {
        try
        {
            // Nunca colocar HARDCODE apenas para fins do desafio
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _validIssuer,
                ValidAudience = _validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            var roles = principal.Claims.Where(c => c.Type == "Role")
                                                         .Select(c => c.Value);

            // Não é a melhor abordagem, pois alguém pode manipuldar o JWT na camada do PAYLOAD, apenas para fins dos desafio
            return roles.Any(role => role.Equals(roleEndpoint, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception)
        {
            return false;
        }
    }
}