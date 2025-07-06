using Entities;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public void SetUser(AppUser? user)
        {
            if (user == null)
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            }
            else
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }, "CustomAuth"));
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }
    }
}
