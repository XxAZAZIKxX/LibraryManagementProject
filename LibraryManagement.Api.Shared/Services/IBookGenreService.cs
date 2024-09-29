using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.AspNetCore.JsonPatch;

namespace LibraryManagement.Api.Shared.Services;

public interface IBookGenreService
{
    Task<BookGenre[]> GetBookGenresAsync();
    Task<Result<BookGenre>> GetBookGenreAsync(Guid genreId);
    Task<BookGenre> AddBookGenreAsync(BookGenreDto request);
    Task<Result<BookGenre>> UpdateBookGenreAsync(Guid genreId, JsonPatchDocument<BookGenreDto> request);
    Task<bool> DeleteBookGenreAsync(Guid genreId);
}