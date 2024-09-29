using System.ComponentModel.DataAnnotations;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Shared.Models;

public class BookGenreDto : ValidatableObjectBase
{
    [StringLength(25, MinimumLength = 3)]
    public string Title { get; set; }

    public BookGenreDto()
    {

    }

    public BookGenreDto(BookGenre genre)
    {
        Title = genre.Title;
    }
}