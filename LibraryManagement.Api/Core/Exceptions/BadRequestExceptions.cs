using System.Net;

namespace LibraryManagement.Api.Core.Exceptions;

public abstract class BadRequestException : ControlledScenarioException
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    protected BadRequestException(string message,
        IDictionary<string, string>? additionalInfo = null) : base(message,
        additionalInfo)
    {
    }
}

public class UsernameIsTakenException(string username) :
    BadRequestException("Username is taken", new Dictionary<string, string>()
    {
        {"username", username}
    });