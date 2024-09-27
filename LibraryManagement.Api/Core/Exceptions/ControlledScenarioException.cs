using System.Collections.Immutable;

namespace LibraryManagement.Api.Core.Exceptions;

public abstract class ControlledScenarioException : Exception
{
    public abstract int StatusCode { get; }
    public IReadOnlyDictionary<string, string> AdditionalInformation { get; } = ImmutableDictionary<string, string>.Empty;

    protected ControlledScenarioException(string message, IDictionary<string, string>? additionalInfo = null) : base(message)
    {
        if(additionalInfo is null)return;
        AdditionalInformation = additionalInfo.ToImmutableDictionary();
    }
}