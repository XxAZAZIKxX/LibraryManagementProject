using LibraryManagement.Api.Data.Models;
using LibraryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Data;

public sealed class DataContext : DbContext
{
    public DbSet<UserAccount> Users { get; private set; }
    public DbSet<UserSalt> UserSalts { get; private set; }
    public DbSet<Book> Books { get; private set; }
    public DbSet<BookAuthor> BookAuthors { get; private set; }
    public DbSet<BookGenre> BookGenres { get; private set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(builder =>
        {
            builder.HasOne(p => p.Genre).WithMany().HasForeignKey(p => p.GenreId);
            builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId);
        });

        base.OnModelCreating(modelBuilder);
    }
}