using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IBookAuthorRepository
{
    Task<BookAuthor[]> GetBookAuthorsAsync();
    Task<Result<BookAuthor>> GetBookAuthorAsync(Guid authorId);
    Task<BookAuthor> AddAuthorAsync(BookAuthor author);
    Task<Result<BookAuthor>> UpdateBookAuthorAsync(Guid authorId, Action<BookAuthor> update);
    Task<bool> DeleteBookAuthorAsync(Guid authorId);
}