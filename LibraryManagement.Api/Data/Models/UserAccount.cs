using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Data.Models;

[Index(nameof(Username), IsUnique = true)]
public class UserAccount
{
    public Guid Id { get; set; }

    [StringLength(25, MinimumLength = 3)] public string Username { get; set; }

    [StringLength(128, MinimumLength = 128)]
    public string PasswordHash { get; set; }
}