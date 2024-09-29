using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;

namespace LibraryManagement.Api.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class AuthorsController(IBookAuthorService authorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<BookAuthor[]>> GetAllBookAuthors()
    {
        return await authorService.GetAuthorsAsync();
    }

    [HttpGet, Route("{authorId:guid}")]
    public async Task<ActionResult<BookAuthor>> GetBookAuthor([FromRoute] Guid authorId)
    {
        var result = await authorService.GetAuthorAsync(authorId);
        return result.Match(author => author, exception => throw exception);
    }

    [HttpPost, Route("add")]
    public async Task<ActionResult<BookAuthor>> AddBookAuthor([FromBody] BookAuthorDto request)
    {
        return await authorService.AddAuthorAsync(request);
    }

    [HttpPatch, Route("{authorId:guid}")]
    public async Task<ActionResult<BookAuthor>> UpdateBookAuthor([FromRoute] Guid authorId,
        [FromBody] JsonPatchDocument<BookAuthorDto> request)
    {
        var result = await authorService.UpdateAuthorAsync(authorId, request);
        return result.Match(author => author, exception => throw exception);
    }

    [HttpDelete, Route("{authorId:guid}")]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid authorId)
    {
        var delete = await authorService.DeleteAuthorAsync(authorId);
        return Ok(new
        {
            Value = delete,
            Comment = delete ? "Deleted" : "Already deleted"
        });
    }
}