using System.Net.Http.Json;

using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

using Skeleton.Shared.Domain.IdentityEntity;

namespace Skeleton.Client.Blazor.Providers;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public AuthStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<Uzivatel>("api/auth/currentUser");
            if (result != null)
            {
                var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, result.UserName),
                new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
            }, "authentication");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

        }
        catch (HttpRequestException)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
