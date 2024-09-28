using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book[]> GetBooksAsync();
    Task<Result<Book>> GetBookAsync(Guid bookId);
    Task<Book> AddBookAsync(Book book);
    Task<Result<Book>> UpdateBookAsync(Guid bookId, Action<Book> updateAction);
    Task<bool> DeleteBookAsync(Guid bookId);
}