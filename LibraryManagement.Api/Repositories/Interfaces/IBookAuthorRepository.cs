using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IBookAuthorRepository
{
    /// <summary>
    /// Get all book authors from repository
    /// </summary>
    /// <returns>
    /// Array of <see cref="BookAuthor"/> objects
    /// </returns>
    Task<BookAuthor[]> GetAuthorsAsync();
    /// <summary>
    /// Get a book from repository
    /// </summary>
    /// <param name="authorId">The ID of the specified book author</param>
    /// <returns>
    /// if founded - <see cref="BookAuthor"/>
    /// </returns>
    /// <exception cref="BookAuthorNotFoundException"></exception>
    Task<Result<BookAuthor>> GetAuthorAsync(Guid authorId);
    /// <summary>
    /// Adds a new book author to repository
    /// </summary>
    /// <param name="author">The new book author</param>
    /// <returns>
    /// Added <see cref="BookAuthor"/>
    /// </returns>
    Task<BookAuthor> AddAuthorAsync(BookAuthor author);
    /// <summary>
    /// Update the existing book author in repository
    /// </summary>
    /// <param name="authorId">The specified book author ID</param>
    /// <param name="update">Action to perform with author</param>
    /// <returns>
    /// if succeed - updated <see cref="BookAuthor"/>
    /// </returns>
    /// <exception cref="BookAuthorNotFoundException"></exception>
    Task<Result<BookAuthor>> UpdateAuthorAsync(Guid authorId, Action<BookAuthor> update);
    /// <summary>
    /// Delete the book author from repository
    /// </summary>
    /// <param name="authorId">The specified book author ID</param>
    /// <returns>
    /// <see langword="true"/> if deleted; 
    /// <see langword="false"/> if already deleted
    /// </returns>
    Task<bool> DeleteAuthorAsync(Guid authorId);
}