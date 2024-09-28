using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Models;

public class Book
{
    public Guid Id { get; set; }
    [StringLength(50, MinimumLength = 3)]
    public string Title { get; set; }
    public BookAuthor Author { get; set; }
    public Guid AuthorId { get; set; }
    public DateOnly PublishDate { get; set; }
    public Guid GenreId { get; set; }
    public BookGenre Genre { get; set; }
}