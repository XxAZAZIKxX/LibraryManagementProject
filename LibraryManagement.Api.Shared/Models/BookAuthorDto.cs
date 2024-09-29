using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;

namespace LibraryManagement.Api.Shared.Models;

public class BookAuthorDto : ValidatableObjectBase
{
    [JsonPropertyName("name")]
    [StringLength(50, MinimumLength = 3)] 
    public string Name { get; set; }

    [JsonPropertyName("surname")]
    [StringLength(50, MinimumLength = 3)] 
    public string Surname { get; set; }

    public BookAuthorDto()
    {
    }

    public BookAuthorDto(BookAuthor author)
    {
        Name = author.Name;
        Surname = author.Surname;
    }
}