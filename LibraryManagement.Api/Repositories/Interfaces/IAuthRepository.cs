using LibraryManagement.Api.Data.Models;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<UserAccount?> GetUserAsync(string username, string passwordHash);
    Task<UserAccount> AddUserAsync(string username, string passwordHash);
    Task<bool> IsUsernameTakenAsync(string username);
}