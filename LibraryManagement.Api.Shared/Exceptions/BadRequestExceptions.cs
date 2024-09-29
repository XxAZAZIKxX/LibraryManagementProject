using System.ComponentModel.DataAnnotations;
using System.Net;

namespace LibraryManagement.Api.Shared.Exceptions;

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
    BadRequestException("Username is taken", new Dictionary<string, string>
    {
        { "username", username }
    });

public class ValidationException(ValidationResult[] validationResults)
    : BadRequestException($"Validation failed for {validationResults.Length} members.",
        GetValidationResults(validationResults))
{
    private static IDictionary<string, string> GetValidationResults(ValidationResult[] results)
    {
        var dictionary = new Dictionary<string, string>(results.Length);

        foreach (var result in results)
        {
            var join = string.Join(", ", result.MemberNames);
            dictionary.Add(join, result.ErrorMessage ?? "Info not included");
        }

        return dictionary;
    }
}