using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IBookRepository
{
    /// <summary>
    ///     Get all books from repository
    /// </summary>
    /// <returns>
    ///     Array of <see cref="Book" /> objects
    /// </returns>
    Task<Book[]> GetBooksAsync();

    /// <summary>
    ///     Get book from repository
    /// </summary>
    /// <param name="bookId">The specified book ID</param>
    /// <returns>
    ///     if found - <see cref="Book" />
    /// </returns>
    /// <exception cref="BookNotFoundException"></exception>
    Task<Result<Book>> GetBookAsync(Guid bookId);

    /// <summary>
    ///     Add new book to repository
    /// </summary>
    /// <param name="book">A new book</param>
    /// <returns>
    ///     Added <see cref="Book" />
    /// </returns>
    Task<Book> AddBookAsync(Book book);

    /// <summary>
    ///     Update the existing book in repository
    /// </summary>
    /// <param name="bookId">The specified book ID</param>
    /// <param name="updateAction">The action to perform</param>
    /// <returns>
    ///     if succeeds - updated <see cref="Book" />
    /// </returns>
    /// <exception cref="BookNotFoundException"></exception>
    Task<Result<Book>> UpdateBookAsync(Guid bookId, Action<Book> updateAction);

    /// <summary>
    ///     Delete the existing book from repository
    /// </summary>
    /// <param name="bookId">The specified book ID</param>
    /// <returns>
    ///     <see langword="true" /> if deleted;
    ///     <see langword="false" /> if already deleted
    /// </returns>
    Task<bool> DeleteBookAsync(Guid bookId);
}