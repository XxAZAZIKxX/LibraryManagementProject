using LibraryManagement.Api.Data;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Repositories;

public class DbBookGenreRepository(DataContext dataContext) : IBookGenreRepository
{
    public async Task<BookGenre[]> GetBookGenresAsync()
    {
        return await dataContext.BookGenres.AsNoTracking().ToArrayAsync();
    }

    public async Task<Result<BookGenre>> GetBookGenreAsync(Guid genreId)
    {
        var singleOrDefault = await dataContext.BookGenres.SingleOrDefaultAsync(p=>p.Id == genreId);
        if (singleOrDefault is null) return new BookGenreNotFoundException(genreId);
        return singleOrDefault;
    }

    public async Task<BookGenre> AddBookGenreAsync(BookGenre bookGenre)
    {
        await dataContext.BookGenres.AddAsync(bookGenre);
        await dataContext.SaveChangesAsync();
        return bookGenre;
    }

    public async Task<Result<BookGenre>> UpdateBookGenreAsync(Guid genreId, Action<BookGenre> updateAction)
    {
        var bookGenreResult = await GetBookGenreAsync(genreId);
        if (bookGenreResult.IsFailed) return bookGenreResult.Exception;

        var bookGenre = bookGenreResult.Value;
        updateAction(bookGenre);

        await dataContext.SaveChangesAsync();
        return bookGenre;
    }

    public async Task<bool> DeleteBookGenreAsync(Guid genreId)
    {
        var bookGenreResult = await GetBookGenreAsync(genreId);
        if (bookGenreResult.IsFailed) return false;

        var bookGenre= bookGenreResult.Value;
        dataContext.BookGenres.Remove(bookGenre);
        await dataContext.SaveChangesAsync();
        return true;
    }
}