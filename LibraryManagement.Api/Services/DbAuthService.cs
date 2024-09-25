using LibraryManagement.Api.Core;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Requests;
using LibraryManagement.Api.Shared.Responses;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Utilities;
using LibraryManagement.Api.Config;
using LibraryManagement.Api.Core.Extensions;

namespace LibraryManagement.Api.Services;

public class DbAuthService(IAuthRepository authRepository, JwtConfig jwtConfig) : IAuthService
{
    public async Task<Result<TokenResponse>> AuthAsync(AuthRequest request)
    {
        var hash = CryptographyTools.GetBytes(request.PasswordHash.ToLowerInvariant()).ToHexString();

        var user = await authRepository.GetUserAsync(request.Username, hash);
        if (user is null) throw new NotImplementedException("User is not exists");
        return new TokenResponse
        {
            UserId = user.Id,
            BearerToken = $"Bearer {jwtConfig.GenerateJwtToken(user.Id)}"
        };
    }

    public async Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request)
    {
        var hash = CryptographyTools.GetBytes(request.PasswordHash.ToLowerInvariant()).ToHexString();
        if (await authRepository.IsUsernameTakenAsync(request.Username))
        {
            throw new NotImplementedException("Username is taken");
        }

        var user = await authRepository.AddUserAsync(request.Username, hash);
        return new TokenResponse()
        {
            UserId = user.Id,
            BearerToken = $"Bearer {jwtConfig.GenerateJwtToken(user.Id)}"
        };
    }

    public Task<Result<TokenResponse>> RefreshTokenAsync(string token)
    {
        throw new NotImplementedException();
    }
}