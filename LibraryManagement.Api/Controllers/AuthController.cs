using LibraryManagement.Api.Shared.Requests;
using LibraryManagement.Api.Shared.Responses;
using LibraryManagement.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost, Route("login")]
    public async Task<ActionResult<TokenResponse>> AuthAsync([FromBody] AuthRequest request)
    {
        var authResult = await authService.AuthAsync(request);
        return authResult.Match(response => response, exception => throw exception);
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult<TokenResponse>> RegisterAsync([FromBody] RegisterRequest request)
    {
        var registerResult = await authService.RegisterAsync(request);
        return registerResult.Match(response => response, exception => throw exception);
    }

    [HttpPost, Route("refresh")]
    public async Task<ActionResult<TokenResponse>> RefreshToken([FromHeader(Name = "Authorization")] string authorization)
    {
        if (authorization.StartsWith("Bearer ")) authorization = authorization[8..];
        var refreshTokenResult = await authService.RefreshTokenAsync(authorization);
        throw new NotImplementedException();
    }
}