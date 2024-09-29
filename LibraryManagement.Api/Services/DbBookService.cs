using LibraryManagement.Api.Repositories.Interfaces;
using LibraryManagement.Api.Shared.Models;
using LibraryManagement.Api.Shared.Services;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Utilities;
using SystemTextJsonPatch;
using ValidationException = LibraryManagement.Api.Shared.Exceptions.ValidationException;

namespace LibraryManagement.Api.Services;

public class DbBookService(
    IBookRepository bookRepository,
    IBookAuthorRepository bookAuthorRepository,
    IBookGenreRepository bookGenreRepository
) : IBookService
{
    public async Task<Book[]> GetBooksAsync()
    {
        return await bookRepository.GetBooksAsync();
    }

    public async Task<Result<Book>> GetBookAsync(Guid bookId)
    {
        return await bookRepository.GetBookAsync(bookId);
    }

    public async Task<Result<Book>> AddBookAsync(BookDto request)
    {
        var authorResult = await bookAuthorRepository.GetAuthorAsync(request.AuthorId);
        var genreResult = await bookGenreRepository.GetBookGenreAsync(request.GenreId);

        if (authorResult.IsFailed) return authorResult.Exception;
        if (genreResult.IsFailed) return genreResult.Exception;

        return await bookRepository.AddBookAsync(new Book
        {
            Author = authorResult.Value,
            Genre = genreResult.Value,
            Title = request.Title,
            PublishDate = request.PublishDate
        });
    }

    public async Task<Result<Book>> UpdateBookAsync(Guid bookId, JsonPatchDocument<BookDto> updateAction)
    {
        var bookResult = await bookRepository.GetBookAsync(bookId);
        if (bookResult.IsFailed) return bookResult.Exception;

        var updateBook = new BookDto(bookResult.Value);
        updateAction.ApplyTo(updateBook);

        if (updateBook.TryValidateObject(out var results) is false)
            return new ValidationException(results.ToArray());

        var author = await bookAuthorRepository.GetAuthorAsync(updateBook.AuthorId);
        var genre = await bookGenreRepository.GetBookGenreAsync(updateBook.GenreId);

        if (author.IsFailed) return author.Exception;
        if (genre.IsFailed) return genre.Exception;

        return await bookRepository.UpdateBookAsync(bookId, book =>
        {
            book.Title = updateBook.Title;
            book.AuthorId = updateBook.AuthorId;
            book.GenreId = updateBook.GenreId;
            book.PublishDate = updateBook.PublishDate;
        });
    }

    public async Task<bool> DeleteBookAsync(Guid bookId)
    {
        return await bookRepository.DeleteBookAsync(bookId);
    }
}