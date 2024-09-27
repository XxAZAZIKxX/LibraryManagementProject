using System.Net;

namespace LibraryManagement.Api.Core.Exceptions;

public abstract class NotFoundException : ControlledScenarioException
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    protected NotFoundException(string message, IDictionary<string, string>? additionalInfo = null) : 
        base(message, additionalInfo)
    {
    }
}

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId) : 
        base($"User with id `{userId}` not found!",
        new Dictionary<string, string> { { "userId", userId.ToString() } })
    {
    }

    public UserNotFoundException(string username) :
        base($"User with username `{username}` not found!",
        new Dictionary<string, string> { { "username", username } })
    {
    }
}

public class UserSaltNotFoundException(Guid userId) : 
    NotFoundException($"User salt for user with id `{userId}` not found", 
        new Dictionary<string, string>()
        {
            {"userId", userId.ToString()}
        });