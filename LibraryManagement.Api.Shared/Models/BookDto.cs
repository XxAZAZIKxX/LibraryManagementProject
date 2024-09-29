using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Shared.Models;

public class BookDto : ValidatableObjectBase
{
    [JsonPropertyName("title")]
    [StringLength(50, MinimumLength = 3)] 
    public string Title { get; set; }
    [JsonPropertyName("publish_date")]
    public DateOnly PublishDate { get; set; }
    [JsonPropertyName("author_id")]
    public Guid AuthorId { get; set; }
    [JsonPropertyName("genre_id")]
    public Guid GenreId { get; set; }

    public BookDto()
    {
    }

    public BookDto(Book book)
    {
        Title = book.Title;
        AuthorId = book.AuthorId;
        GenreId = book.GenreId;
        PublishDate = book.PublishDate;
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (PublishDate == default) 
            results.Add(new ValidationResult("Publish date is not set", [nameof(PublishDate)]));

        if(AuthorId == default)
            results.Add(new ValidationResult("Author id is not set", [nameof(AuthorId)]));

        if(GenreId == default)
            results.Add(new ValidationResult("Genre id is not set", [nameof(GenreId)]));

        return results;
    }
}