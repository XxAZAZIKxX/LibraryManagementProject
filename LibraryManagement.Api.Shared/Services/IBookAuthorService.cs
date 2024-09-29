using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.AspNetCore.JsonPatch;

namespace LibraryManagement.Api.Shared.Services;

public interface IBookAuthorService
{
    /// <summary>
    /// Get all book authors
    /// </summary>
    /// <returns>
    /// Array of <see cref="BookAuthor"/> objects
    /// </returns>
    Task<BookAuthor[]> GetAuthorsAsync();
    /// <summary>
    /// Get book author
    /// </summary>
    /// <param name="authorId">The specified book author ID</param>
    /// <returns>
    /// if found - <see cref="BookAuthor"/>
    /// </returns>
    /// <exception cref="BookAuthorNotFoundException"></exception>
    Task<Result<BookAuthor>> GetAuthorAsync(Guid authorId);
    /// <summary>
    /// Add a new book author
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// Added book author
    /// </returns>
    Task<BookAuthor> AddAuthorAsync(BookAuthorDto request);
    /// <summary>
    /// Update the existing book author
    /// </summary>
    /// <param name="authorId">The specified author ID</param>
    /// <param name="request"></param>
    /// <returns>
    /// if found - <see cref="BookAuthor"/>
    /// </returns>
    Task<Result<BookAuthor>> UpdateAuthorAsync(Guid authorId, JsonPatchDocument<BookAuthorDto> request);
    /// <summary>
    /// Delete the existing book author
    /// </summary>
    /// <param name="authorId">The specified author ID</param>
    /// <returns>
    /// <see langword="true"/> if deleted; 
    /// <see langword="false"/> if already deleted
    /// </returns>
    Task<bool> DeleteAuthorAsync(Guid authorId);
}