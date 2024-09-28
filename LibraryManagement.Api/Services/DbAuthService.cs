using System.Security.Claims;
using System.Security.Cryptography;
using LibraryManagement.Api.Core;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Responses;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Utilities;
using LibraryManagement.Api.Config;
using LibraryManagement.Api.Core.Exceptions;
using LibraryManagement.Api.Core.Extensions;
using LibraryManagement.Api.Data;
using LibraryManagement.Api.Data.Models;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Api.Shared.Requests.Auth;

namespace LibraryManagement.Api.Services;

public class DbAuthService(
    IUserRepository userRepository,
    IUserSaltRepository userSaltRepository,
    DbUnitOfWork<DataContext> dbUnitOfWork,
    JwtConfig jwtConfig
    ) : IAuthService
{
    public async Task<Result<TokenResponse>> AuthAsync(AuthRequest request)
    {
        var userResult = await userRepository.GetUserAsync(request.Username);

        if (userResult.IsFailed) return new InvalidCredentialsException();

        var user = userResult.Value;

        var userSaltResult = await userSaltRepository.GetUserSaltAsync(user.Id);
        if (userSaltResult.IsFailed) return userSaltResult.Exception;

        var hash = CryptographyTools
            .GetBytes(request.PasswordHash, userSaltResult.Value.SaltBytes, 50_000)
            .ToHexString();

        if (user.PasswordHash != hash) return new InvalidCredentialsException();

        return new TokenResponse
        {
            UserId = user.Id,
            BearerToken = $"Bearer {jwtConfig.GenerateJwtToken(user.Id)}"
        };
    }

    public async Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request)
    {
        if (await userRepository.IsUsernameTakenAsync(request.Username))
        {
            return new UsernameIsTakenException(request.Username);
        }

        await using var transaction = dbUnitOfWork.BeginTransaction();
        try
        {
            var user = await userRepository.AddUserAsync(new UserAccount
            {
                Username = request.Username,
                PasswordHash = ""
            });

            var userSalt = await userSaltRepository.AddUserSaltAsync(new UserSalt()
            {
                User = user,
                SaltBytes = RandomNumberGenerator.GetBytes(64)
            });

            var updateResult = await userRepository.UpdateUserAsync(user.Id, account =>
            {
                account.PasswordHash = CryptographyTools
                    .GetBytes(request.PasswordHash, userSalt.SaltBytes, 50_000)
                    .ToHexString();
            });
            if (updateResult.IsFailed) throw updateResult.Exception;

            await transaction.CommitAsync();
            return new TokenResponse
            {
                UserId = user.Id,
                BearerToken = $"Bearer {jwtConfig.GenerateJwtToken(user.Id)}"
            };
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return e;
        }
    }

    public Task<Result<TokenResponse>> RefreshTokenAsync(string token)
    {
        var valid = jwtConfig.IsJwtTokenSignatureValid(token, out var principal);
        if (valid is false || principal is null) 
            return Task.FromResult<Result<TokenResponse>>(new InvalidTokenException());

        var userId = Guid.Parse(principal.Claims.GetUserId());

        return Task.FromResult<Result<TokenResponse>>(new TokenResponse
        {
            UserId = userId,
            BearerToken = $"Bearer {jwtConfig.GenerateJwtToken(userId)}"
        });
    }
}