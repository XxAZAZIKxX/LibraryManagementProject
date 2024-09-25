using System.Text;
using LibraryManagement.Api.Core;
using LibraryManagement.Api.Core.Extensions;
using LibraryManagement.Api.Data;
using LibraryManagement.Api.Data.Models;
using LibraryManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Repositories;

public class DbAuthRepository(DataContext dataContext) : IAuthRepository
{
    public async Task<UserAccount?> GetUserAsync(string username, string passwordHash)
    {
        return await dataContext.Users
            .FirstOrDefaultAsync(p => p.Username == username &&
                                      p.PasswordHash == passwordHash);
    }

    public async Task<UserAccount> AddUserAsync(string username, string passwordHash)
    {
        var userAccount = new UserAccount()
        {
            Username = username,
            PasswordHash = passwordHash
        };
        await dataContext.Users.AddAsync(userAccount);
        await dataContext.SaveChangesAsync();
        return userAccount;
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await dataContext.Users.AnyAsync(p => p.Username == username);
    }
}