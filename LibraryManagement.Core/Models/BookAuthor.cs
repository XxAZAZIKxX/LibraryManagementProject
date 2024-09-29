using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Models;

public class BookAuthor
{
    public Guid Id { get; set; }

    [StringLength(50, MinimumLength = 3)] public string Name { get; set; }

    [StringLength(50, MinimumLength = 3)] public string Surname { get; set; }
}