using System.Security.Claims;

namespace API.Infrastracture.Security;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal @this)
    {
        return @this.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
