namespace LibraryManagement.Api.Core.Exceptions;

public class ConfigurationSectionNotFoundException(string path) 
    : Exception($"Section with path `{path}` is missing!");

public class ConfigurationSectionValueIsMissingException(string path)
    : Exception($"Section value with path `{path}` is missing!");