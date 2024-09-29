using LibraryManagement.Api.Data;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Exceptions;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Repositories;

public class DbBookAuthorRepository(DataContext dataContext) : IBookAuthorRepository
{
    public async Task<BookAuthor[]> GetAuthorsAsync()
    {
        return await dataContext.BookAuthors.AsNoTracking().ToArrayAsync();
    }

    public async Task<Result<BookAuthor>> GetAuthorAsync(Guid authorId)
    {
        var singleOrDefault = await dataContext.BookAuthors.SingleOrDefaultAsync(p=>p.Id == authorId);
        if (singleOrDefault is null) return new BookAuthorNotFoundException(authorId);
        return singleOrDefault;
    }

    public async Task<BookAuthor> AddAuthorAsync(BookAuthor author)
    {
        await dataContext.BookAuthors.AddAsync(author);
        await dataContext.SaveChangesAsync();
        return author;
    }

    public async Task<Result<BookAuthor>> UpdateAuthorAsync(Guid authorId, Action<BookAuthor> update)
    {
        var authorResult = await GetAuthorAsync(authorId);
        if (authorResult.IsFailed) return authorResult.Exception;

        var author = authorResult.Value;
        update(author);

        await dataContext.SaveChangesAsync();
        return author;
    }

    public async Task<bool> DeleteAuthorAsync(Guid authorId)
    {
        var authorResult = await GetAuthorAsync(authorId);
        if (authorResult.IsFailed) return false;

        var author = authorResult.Value;

        dataContext.BookAuthors.Remove(author);
        await dataContext.SaveChangesAsync();
        return true;
    }
}