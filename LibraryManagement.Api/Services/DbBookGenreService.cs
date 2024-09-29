using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.AspNetCore.JsonPatch;
using ValidationException = LibraryManagement.Api.Shared.Exceptions.ValidationException;

namespace LibraryManagement.Api.Services;

public class DbBookGenreService(IBookGenreRepository genreRepository) : IBookGenreService
{
    public async Task<BookGenre[]> GetBookGenresAsync()
    {
        return await genreRepository.GetBookGenresAsync();
    }

    public async Task<Result<BookGenre>> GetBookGenreAsync(Guid genreId)
    {
        return await genreRepository.GetBookGenreAsync(genreId);
    }

    public async Task<BookGenre> AddBookGenreAsync(BookGenreDto request)
    {
        return await genreRepository.AddBookGenreAsync(new BookGenre()
        {
            Title = request.Title
        });
    }

    public async Task<Result<BookGenre>> UpdateBookGenreAsync(Guid genreId, JsonPatchDocument<BookGenreDto> request)
    {
        var result = await genreRepository.GetBookGenreAsync(genreId);
        if (result.IsFailed) return result.Exception;

        var genreDto = new BookGenreDto(new BookGenre());

        if (genreDto.TryValidateObject(out var results) is false)
            return new ValidationException(results.ToArray());

        return await genreRepository.UpdateBookGenreAsync(genreId, genre => { genre.Title = genreDto.Title; });
    }

    public async Task<bool> DeleteBookGenreAsync(Guid genreId)
    {
        return await genreRepository.DeleteBookGenreAsync(genreId);
    }
}