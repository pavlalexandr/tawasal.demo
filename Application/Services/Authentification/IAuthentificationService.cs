using System.Security.Claims;

namespace Application.Services
{
    public interface IAuthentificationService
    {
        ClaimsPrincipal? TryGetClaimPrincipal(string username, string password);
    }
}
