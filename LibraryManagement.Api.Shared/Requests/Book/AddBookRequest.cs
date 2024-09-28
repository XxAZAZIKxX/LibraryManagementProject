using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagement.Api.Shared.Requests.Book;

public class AddBookRequest
{
    [JsonPropertyName("title")]
    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; init; }
    [JsonPropertyName("publish_date")]
    public required DateOnly PublishDate { get; init; }
    [JsonPropertyName("author_id")]
    public required Guid AuthorId { get; init; }
    [JsonPropertyName("genre_id")]
    public required Guid GenreId { get; init; }
}