using LibraryManagement.Api.Shared.Requests;
using LibraryManagement.Api.Shared.Responses;
using LibraryManagement.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[ApiController, Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost, Route("auth")]
    public async Task<ActionResult<TokenResponse>> AuthAsync([FromBody] AuthRequest request)
    {
        var authResult = await authService.AuthAsync(request);
        return authResult.Match(response => response, exception => throw exception);
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult<TokenResponse>> RegisterAsync([FromBody] RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}