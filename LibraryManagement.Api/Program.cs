using LibraryManagement.Api.Config;
using LibraryManagement.Api.Core.Extensions;
using LibraryManagement.Api.Data;
using LibraryManagement.Api.Repositories;
using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Services;
using LibraryManagement.Api.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>((provider, optionsBuilder) =>
{
    var config = provider.GetRequiredService<MySqlDatabaseConfig>();

    var connectionString = new MySqlConnectionStringBuilder()
    {
        Server = config.Server,
        Database = config.Database,
        UserID = config.User,
        Password = config.Password
    }.ConnectionString;

    optionsBuilder
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<IAuthRepository, DbAuthRepository>();
builder.Services.AddScoped<IAuthService, DbAuthService>();

builder.Services.LoadConfigsFromAssembly();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler();

app.Run();
