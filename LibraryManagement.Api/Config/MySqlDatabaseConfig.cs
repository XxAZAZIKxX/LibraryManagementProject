using LibraryManagement.Api.Core.Extensions;

namespace LibraryManagement.Api.Config;

public sealed class MySqlDatabaseConfig(IConfiguration configuration) : IConfig
{
    public string Database { get; } = configuration.GetValueOrThrow("Database:Database");
    public string Server { get; } = configuration.GetValueOrThrow("Database:Server");
    public string User { get; } = configuration.GetValueOrThrow("Database:User");
    public string Password { get; } = configuration.GetValueOrThrow("Database:Password");
}