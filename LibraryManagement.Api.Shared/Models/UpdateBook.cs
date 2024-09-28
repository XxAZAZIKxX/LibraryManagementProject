using System.ComponentModel.DataAnnotations;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Shared.Models;

public class UpdateBook : ValidatableObjectBase
{
    [StringLength(50, MinimumLength = 3)]
    public string Title { get; set; }
    public DateOnly PublishDate { get; set; }
    public Guid AuthorId { get; set; }
    public Guid GenreId { get; set; }

    public UpdateBook()
    {

    }

    public UpdateBook(Book book)
    {
        Title = book.Title;
        AuthorId = book.AuthorId;
        GenreId = book.GenreId;
        PublishDate = book.PublishDate;
    }
}