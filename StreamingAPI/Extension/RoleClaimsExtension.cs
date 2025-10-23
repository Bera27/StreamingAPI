using System.Security.Claims;
using StreamingAPI.Models;

namespace StreamingAPI.Extension
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this Usuario usuario)
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Email)
            };

            result.AddRange(usuario.Roles.Select(role => new Claim(ClaimTypes.Role, role.Nome)));

            return result;
        }
    }
}