using LibraryManagement.Api.Data.Models;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IUserRepository
{
    /// <summary>
    ///     Get user from repository
    /// </summary>
    /// <param name="userId">The specified user ID</param>
    /// <returns>
    ///     if found - <see cref="UserAccount" />
    /// </returns>
    /// <exception cref="UserNotFoundException"></exception>
    Task<Result<UserAccount>> GetUserAsync(Guid userId);

    /// <summary>
    ///     Get user from repository
    /// </summary>
    /// <param name="username">The specified user username</param>
    /// <returns>
    ///     if found - <see cref="UserAccount" />
    /// </returns>
    /// <exception cref="UserNotFoundException"></exception>
    Task<Result<UserAccount>> GetUserAsync(string username);

    /// <summary>
    ///     Add new user to repository
    /// </summary>
    /// <param name="userAccount">A new user account</param>
    /// <returns>
    ///     Added <see cref="UserAccount" />
    /// </returns>
    Task<UserAccount> AddUserAsync(UserAccount userAccount);

    /// <summary>
    ///     Update the existing user in repository
    /// </summary>
    /// <param name="userId">The specified user ID</param>
    /// <param name="updateAction">The action to perform</param>
    /// <returns>
    ///     if succeeds - updated <see cref="UserAccount" />
    /// </returns>
    /// <exception cref="UserNotFoundException"></exception>
    Task<Result<UserAccount>> UpdateUserAsync(Guid userId, Action<UserAccount> updateAction);

    /// <summary>
    ///     Check is username is available
    /// </summary>
    /// <param name="username">The username to check</param>
    /// <returns>
    ///     <see langword="true" /> if username is taken;
    ///     otherwise <see langword="false" />
    /// </returns>
    Task<bool> IsUsernameTakenAsync(string username);
}