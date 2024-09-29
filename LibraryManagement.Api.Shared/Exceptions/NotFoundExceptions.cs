using System.Net;

namespace LibraryManagement.Api.Shared.Exceptions;

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
    NotFoundException($"User salt for user with id `{userId}` not found!",
        new Dictionary<string, string>
        {
            { "userId", userId.ToString() }
        });

public class BookNotFoundException(Guid bookId) : NotFoundException($"Book with id `{bookId}` not found!",
    new Dictionary<string, string>
    {
        { "bookId", bookId.ToString() }
    });

public class BookAuthorNotFoundException(Guid authorId) : NotFoundException(
    $"Book author with id `{authorId}` not found!",
    new Dictionary<string, string>
    {
        { "authorId", authorId.ToString() }
    });

public class BookGenreNotFoundException(Guid genreId) : NotFoundException($"Book genre with id `{genreId}` not found!",
    new Dictionary<string, string>
    {
        { "genreId", genreId.ToString() }
    });