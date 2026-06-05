using LibraryApi1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi1.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
        public DbSet<Borrow> Borrows => Set<Borrow>();
          protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });
        }
    }
}
