using LibraryManagement.Api.Shared.Requests.Auth;
using LibraryManagement.Api.Shared.Responses;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Shared.Services;

public interface IAuthService
{
    Task<Result<TokenResponse>> AuthAsync(AuthRequest request);
    Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request);
    Task<Result<TokenResponse>> RefreshTokenAsync(string token);
}