using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using SystemTextJsonPatch;
using ValidationException = LibraryManagement.Api.Shared.Exceptions.ValidationException;

namespace LibraryManagement.Api.Services;

public class DbBookAuthorService(IBookAuthorRepository authorRepository) : IBookAuthorService
{
    public async Task<BookAuthor[]> GetAuthorsAsync()
    {
        return await authorRepository.GetAuthorsAsync();
    }

    public async Task<Result<BookAuthor>> GetAuthorAsync(Guid authorId)
    {
        return await authorRepository.GetAuthorAsync(authorId);
    }

    public async Task<BookAuthor> AddAuthorAsync(BookAuthorDto request)
    {
        return await authorRepository.AddAuthorAsync(new BookAuthor
        {
            Name = request.Name,
            Surname = request.Surname
        });
    }

    public async Task<Result<BookAuthor>> UpdateAuthorAsync(Guid authorId, JsonPatchDocument<BookAuthorDto> request)
    {
        var authorResult = await GetAuthorAsync(authorId);
        if (authorResult.IsFailed) return authorResult.Exception;

        var author = new BookAuthorDto(authorResult.Value);
        request.ApplyTo(author);
        if (author.TryValidateObject(out var results) is false)
            return new ValidationException(results.ToArray());

        return await authorRepository.UpdateAuthorAsync(authorId, bookAuthor =>
        {
            bookAuthor.Name = author.Name;
            bookAuthor.Surname = author.Surname;
        });
    }

    public async Task<bool> DeleteAuthorAsync(Guid authorId)
    {
        return await authorRepository.DeleteAuthorAsync(authorId);
    }
}