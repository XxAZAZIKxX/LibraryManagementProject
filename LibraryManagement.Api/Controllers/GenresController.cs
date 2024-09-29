using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;

namespace LibraryManagement.Api.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class GenresController(IBookGenreService genreService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<BookGenre[]>> GetAllBookGenres()
    {
        return await genreService.GetBookGenresAsync();
    }

    [HttpGet, Route("{genreId:guid}")]
    public async Task<ActionResult<BookGenre>> GetBookGenre([FromRoute] Guid genreId)
    {
        var result = await genreService.GetBookGenreAsync(genreId);
        return result.Match(genre => genre, exception => throw exception);
    }

    [HttpPost, Route("add")]
    public async Task<ActionResult<BookGenre>> AddBookGenre([FromBody] BookGenreDto request)
    {
        return await genreService.AddBookGenreAsync(request);
    }

    [HttpPatch, Route("{genreId:guid}")]
    public async Task<ActionResult<BookGenre>> UpdateBookGenre([FromRoute] Guid genreId,
        [FromBody] JsonPatchDocument<BookGenreDto> request)
    {
        var result = await genreService.UpdateBookGenreAsync(genreId, request);
        return result.Match(genre => genre, exception => throw exception);
    }

    [HttpDelete, Route("{genreId:guid}")]
    public async Task<IActionResult> DeleteBookGenre([FromRoute] Guid genreId)
    {
        var delete = await genreService.DeleteBookGenreAsync(genreId);
        return Ok(new
        {
            Value = delete,
            Comment = delete ? "Deleted" : "Already deleted"
        });
    }
}