using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Models;

public class BookGenre
{
    public Guid Id { get; set; }

    [StringLength(25, MinimumLength = 3)] public string Title { get; set; }
}