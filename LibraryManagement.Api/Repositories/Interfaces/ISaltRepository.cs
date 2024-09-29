using LibraryManagement.Api.Data.Models;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IUserSaltRepository
{
    /// <summary>
    ///     Get user salt from repository
    /// </summary>
    /// <param name="userId">The specified user ID</param>
    /// <returns>
    ///     if found - <see cref="UserSalt" />
    /// </returns>
    /// <exception cref="UserNotFoundException"></exception>
    Task<Result<UserSalt>> GetUserSaltAsync(Guid userId);

    /// <summary>
    ///     Add new user salt to repository
    /// </summary>
    /// <param name="userSalt">A new user salt</param>
    /// <returns>
    ///     Added <see cref="UserSalt" />
    /// </returns>
    Task<UserSalt> AddUserSaltAsync(UserSalt userSalt);
}