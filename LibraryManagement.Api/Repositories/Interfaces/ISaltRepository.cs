using LibraryManagement.Api.Data.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IUserSaltRepository
{
    Task<Result<UserSalt>> GetUserSaltAsync(Guid userId);
    Task<UserSalt> AddUserSaltAsync(UserSalt userSalt);
}