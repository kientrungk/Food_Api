using System.Security.Claims;

namespace ApiWebFood.Data
{
    public static class IdentityExtention
    {
        public static String Getid(this ClaimsPrincipal user)
            => user.Claims.
            FirstOrDefault(id => id.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
