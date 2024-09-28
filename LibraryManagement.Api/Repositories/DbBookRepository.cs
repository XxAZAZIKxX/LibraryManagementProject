using LibraryManagement.Api.Data;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Repositories;

public class DbBookRepository(DataContext dataContext) : IBookRepository
{
    public async Task<Book[]> GetBooksAsync()
    {
        return await dataContext.Books
            .Include(p => p.Author)
            .Include(p => p.Genre)
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task<Result<Book>> GetBookAsync(Guid bookId)
    {
        var singleOrDefault = await dataContext.Books
            .Include(p => p.Author)
            .Include(p => p.Genre)
            .SingleOrDefaultAsync(p => p.Id == bookId);

        if (singleOrDefault is null) return new BookNotFoundException(bookId);

        return singleOrDefault;
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        await dataContext.Books.AddAsync(book);
        await dataContext.SaveChangesAsync();
        return book;
    }

    public async Task<Result<Book>> UpdateBookAsync(Guid bookId, Action<Book> updateAction)
    {
        var result = await GetBookAsync(bookId);
        if (result.IsFailed) return result.Exception;
        var book = result.Value;

        updateAction(book);

        await dataContext.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteBookAsync(Guid bookId)
    {
        var book = await GetBookAsync(bookId);
        if (book.IsFailed) return false;
        dataContext.Books.Remove(book.Value);
        await dataContext.SaveChangesAsync();
        return true;
    }
}