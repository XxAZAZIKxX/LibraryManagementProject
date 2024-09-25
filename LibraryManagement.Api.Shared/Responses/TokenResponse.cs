namespace LibraryManagement.Api.Shared.Responses;

public class TokenResponse
{
    public Guid UserId { get; set; }
    public string BearerToken { get; set; }
}