using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Api.Shared.Requests.Book;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Book[]>> GetAllBooks()
    {
        return await bookService.GetBooksAsync();
    }

    [HttpGet, Route("{bookId:guid}")]
    public async Task<ActionResult<Book>> GetBook([FromRoute] Guid bookId)
    {
        var result = await bookService.GetBookAsync(bookId);
        return result.Match(book => book, exception => throw exception);
    }

    [HttpPost, Route("add")]
    public async Task<ActionResult<Book>> AddBook([FromBody] AddBookRequest request)
    {
        var result = await bookService.AddBookAsync(request);
        return result.Match(book => book, exception => throw exception);
    }

    [HttpPatch, Route("{bookId:guid}")]
    public async Task<ActionResult<Book>> UpdateBook([FromRoute] Guid bookId, JsonPatchDocument<UpdateBook> updateBook)
    {
        var result = await bookService.UpdateBookAsync(bookId, updateBook);
        return result.Match(book => book, exception => throw exception);
    }

    [HttpDelete, Route("{bookId:guid}")]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid bookId)
    {
        var result = await bookService.DeleteBookAsync(bookId);
        return Ok(result ? "Deleted!" : "Already deleted!");
    }
}