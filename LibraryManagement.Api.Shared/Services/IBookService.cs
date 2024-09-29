using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.AspNetCore.JsonPatch;

namespace LibraryManagement.Api.Shared.Services;

public interface IBookService
{
    /// <summary>
    /// Get all books
    /// </summary>
    /// <returns>
    /// Array of <see cref="Book"/> objects
    /// </returns>
    Task<Book[]> GetBooksAsync();
    /// <summary>
    /// Get a book by ID
    /// </summary>
    /// <param name="bookId">The requested book ID</param>
    /// <returns>
    /// if founded - <see cref="Book"/>
    /// </returns>
    /// <exception cref="BookNotFoundException"></exception>
    Task<Result<Book>> GetBookAsync(Guid bookId);
    /// <summary>
    /// Add a new book
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// if succeeds - the new <see cref="Book"/>
    /// </returns>
    /// <exception cref="BookAuthorNotFoundException"></exception>
    /// <exception cref="BookGenreNotFoundException"></exception>
    Task<Result<Book>> AddBookAsync(BookDto request);
    /// <summary>
    /// Updates the existing book
    /// </summary>
    /// <param name="bookId">The requested book ID</param>
    /// <param name="updateAction">The action to perform</param>
    /// <returns>
    /// if succeeds - the updated <see cref="Book"/>
    /// </returns>
    /// <exception cref="BookAuthorNotFoundException"></exception>
    /// <exception cref="BookGenreNotFoundException"></exception>
    Task<Result<Book>> UpdateBookAsync(Guid bookId, JsonPatchDocument<BookDto> updateAction);
    /// <summary>
    /// Deletes a book
    /// </summary>
    /// <param name="bookId">The requested book ID</param>
    /// <returns>
    /// <see langword="true"/> if deleted;
    /// <see langword="false"/> if book is already deleted
    /// </returns>
    Task<bool> DeleteBookAsync(Guid bookId);
}