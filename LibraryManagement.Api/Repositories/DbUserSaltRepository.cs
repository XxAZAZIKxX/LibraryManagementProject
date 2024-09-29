using LibraryManagement.Api.Data;
using LibraryManagement.Api.Data.Models;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Repositories;

public class DbUserSaltRepository(DataContext dataContext) : IUserSaltRepository
{
    public async Task<Result<UserSalt>> GetUserSaltAsync(Guid userId)
    {
        var userSalt = await dataContext.UserSalts.SingleOrDefaultAsync(p => p.UserId == userId);
        if (userSalt is null) return new UserSaltNotFoundException(userId);
        return userSalt;
    }

    public async Task<UserSalt> AddUserSaltAsync(UserSalt userSalt)
    {
        await dataContext.UserSalts.AddAsync(userSalt);
        await dataContext.SaveChangesAsync();
        return userSalt;
    }
}