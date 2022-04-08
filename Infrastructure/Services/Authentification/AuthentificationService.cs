using Application.Services;
using Domain.Models.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Infrastructure.Services.Authentification
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly ApplicationSettingsModel _applicationSettingsModel;
        private ClaimsPrincipal? _claimsPrincipalCache = null;

        public AuthentificationService(ApplicationSettingsModel applicationSettingsModel)
        {
            _applicationSettingsModel = applicationSettingsModel;
        }

        public ClaimsPrincipal? TryGetClaimPrincipal(string username, string password)
        {
            if (_applicationSettingsModel.Password == password && username.ToLower() == _applicationSettingsModel.Login.ToLower())
            {
                if (_claimsPrincipalCache != null) return _claimsPrincipalCache;

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, username),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                return _claimsPrincipalCache =  new ClaimsPrincipal(identity);
            }

            return null;
        }
    }
}
