using LibraryManagement.Api.Data;
using LibraryManagement.Api.Data.Models;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Repositories;

public class DbUserRepository(DataContext dataContext) : IUserRepository
{
    public async Task<Result<UserAccount>> GetUserAsync(Guid userId)
    {
        var user = await dataContext.Users.SingleOrDefaultAsync(p => p.Id == userId);
        if (user is null) return new UserNotFoundException(userId);
        return user;
    }

    public async Task<Result<UserAccount>> GetUserAsync(string username)
    {
        var user = await dataContext.Users.SingleOrDefaultAsync(p => p.Username == username);
        if (user is null) return new UserNotFoundException(username);
        return user;
    }

    public async Task<UserAccount> AddUserAsync(UserAccount userAccount)
    {
        await dataContext.Users.AddAsync(userAccount);
        await dataContext.SaveChangesAsync();
        return userAccount;
    }

    public async Task<Result<UserAccount>> UpdateUserAsync(Guid userId, Action<UserAccount> updateAction)
    {
        var userResult = await GetUserAsync(userId);
        if (userResult.IsFailed) return userResult.Exception;

        var user = userResult.Value;
        updateAction(user);

        await dataContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await dataContext.Users.AnyAsync(p => p.Username == username);
    }
}