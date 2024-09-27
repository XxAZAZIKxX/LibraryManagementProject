using System.Runtime.InteropServices.JavaScript;
using LibraryManagement.Api.Data.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<Result<UserAccount>> GetUserAsync(Guid userId);
    Task<Result<UserAccount>> GetUserAsync(string username);
    Task<UserAccount> AddUserAsync(UserAccount userAccount);
    Task<Result<UserAccount>> UpdateUserAsync(Guid userId, Action<UserAccount> updateAction);
    Task<bool> IsUsernameTakenAsync(string username);
}