using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Shared.Requests.Auth;

public class RegisterRequest
{
    [StringLength(25, MinimumLength = 3)]
    public required string Username { get; init; }
    [StringLength(128, MinimumLength = 128)]
    public required string PasswordHash { get; init; }
}