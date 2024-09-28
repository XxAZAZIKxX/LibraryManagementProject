﻿namespace LibraryManagement.Api.Shared.Responses;

public class TokenResponse
{
    public required Guid UserId { get; init; }
    public required string BearerToken { get; init; }
}