using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IBookGenreRepository
{
    Task<BookGenre[]> GetBookGenresAsync();
    Task<Result<BookGenre>> GetBookGenreAsync(Guid genreId);
    Task<BookGenre> AddBookGenreAsync(BookGenre bookGenre);
    Task<Result<BookGenre>> UpdateBookGenreAsync(Guid genreId, Action<BookGenre> updateAction);
    Task<bool> DeleteBookGenreAsync(Guid genreId);
}