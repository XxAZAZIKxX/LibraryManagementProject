using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Api.Shared.Requests.Auth;
using LibraryManagement.Api.Shared.Responses;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Shared.Services;

public interface IAuthService
{
    /// <summary>
    /// Login in existing account
    /// </summary>
    /// <param name="request">Login credentials</param>
    /// <returns>
    /// <see cref="TokenResponse"/> if login succeeds
    /// </returns>
    /// <exception cref="InvalidCredentialsException"></exception>
    Task<Result<TokenResponse>> AuthAsync(AuthRequest request);
    /// <summary>
    /// Register and login in new account
    /// </summary>
    /// <param name="request">Register credentials</param>
    /// <returns>
    /// <see cref="TokenResponse"/> if register succeeds
    /// </returns>
    /// <exception cref="UsernameIsTakenException"></exception>
    Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request);
    /// <summary>
    /// Refresh an existing token
    /// </summary>
    /// <param name="token">Old token to refresh</param>
    /// <returns>
    /// <see cref="TokenResponse"/> if refresh succeeds
    /// </returns>
    /// <exception cref="InvalidTokenException"></exception>
    Task<Result<TokenResponse>> RefreshTokenAsync(string token);
}