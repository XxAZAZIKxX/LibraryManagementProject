using System.Net;

namespace LibraryManagement.Api.Core.Exceptions;

public abstract class UnauthorizedException : ControlledScenarioException
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    protected UnauthorizedException(string message,
        IDictionary<string, string>? additionalInfo = null) : base(message,
        additionalInfo)
    {
    }
}

public class InvalidCredentialsException() : UnauthorizedException("Invalid credentials!");

public class InvalidTokenException() : UnauthorizedException("Invalid token!");