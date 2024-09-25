using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Shared.Requests;

public class RegisterRequest
{
    [StringLength(25, MinimumLength = 3)]
    public string Username { get; set; }
    [StringLength(128, MinimumLength = 128)]
    public string PasswordHash { get; set; }
}