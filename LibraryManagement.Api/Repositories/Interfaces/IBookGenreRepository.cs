using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IBookGenreRepository
{
    /// <summary>
    /// Get all book genres from repository
    /// </summary>
    /// <returns>
    /// Array of <see cref="BookGenre"/> objects
    /// </returns>
    Task<BookGenre[]> GetBookGenresAsync();
    /// <summary>
    /// Get book genre from repository
    /// </summary>
    /// <param name="genreId">The specified book genre ID</param>
    /// <returns>
    /// if found - <see cref="BookGenre"/>>
    /// </returns>
    /// <exception cref="BookGenreNotFoundException"></exception>
    Task<Result<BookGenre>> GetBookGenreAsync(Guid genreId);
    /// <summary>
    /// Add a new book genre to repository
    /// </summary>
    /// <param name="bookGenre">A new book genre</param>
    /// <returns>
    /// Added <see cref="BookGenre"/>
    /// </returns>
    Task<BookGenre> AddBookGenreAsync(BookGenre bookGenre);
    /// <summary>
    /// Update the existing book genre in repository
    /// </summary>
    /// <param name="genreId">The specified book genre ID</param>
    /// <param name="updateAction">The action to perform</param>
    /// <returns>
    /// if succeeds - updated <see cref="BookGenre"/>
    /// </returns>
    /// <exception cref="BookGenreNotFoundException"></exception>
    Task<Result<BookGenre>> UpdateBookGenreAsync(Guid genreId, Action<BookGenre> updateAction);
    /// <summary>
    /// Delete the existing book genre from repository
    /// </summary>
    /// <param name="genreId">The specified book genre ID</param>
    /// <returns>
    /// <see langword="true"/> if deleted; 
    /// <see langword="false"/> if already deleted
    /// </returns>
    Task<bool> DeleteBookGenreAsync(Guid genreId);
}