using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Api.Shared.Requests.Book;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using Microsoft.AspNetCore.JsonPatch;

namespace LibraryManagement.Api.Shared.Services;

public interface IBookService
{
    Task<Book[]> GetBooksAsync();
    Task<Result<Book>> GetBookAsync(Guid bookId);
    Task<Result<Book>> AddBookAsync(AddBookRequest request);
    Task<Result<Book>> UpdateBookAsync(Guid bookId, JsonPatchDocument<UpdateBook> updateAction);
    Task<bool> DeleteBookAsync(Guid bookId);
}