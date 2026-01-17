using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Illustrator> Illustrators { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Person inheritance using Table-per-Concrete-Class (TPC) approach
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();

        // Book -> Illustrator (many-to-one)
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Illustrator)
            .WithMany(i => i.Books)
            .HasForeignKey(b => b.IllustratorId);

        // Book -> Illustrator (many-to-one)
        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired();

        // ISBN (Book) is unique
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

        // Couple Title and PublicationYear (Book) is unique
        modelBuilder.Entity<Book>()
            .HasIndex(b => new { b.Title, b.PublicationYear })
            .IsUnique();

        // Couple BookId and AuthorId as key of BookAuthor entity
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        // Couple BookId and AuthorId (BookAuthor) is unique
        modelBuilder.Entity<BookAuthor>()
            .HasIndex(ba => new { ba.AuthorId, ba.BookId })
            .IsUnique();

        // Init. of Author data
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FirstName = "Stephen", LastName = "King" },
            new Author { Id = 2, FirstName = "Marguerite", LastName = "Duras" },
            new Author { Id = 3, FirstName = "Isaac", LastName = "Asimov" }
        );

        // Init. of Illustrator data
        modelBuilder.Entity<Illustrator>().HasData(
            new Illustrator { Id = 4, FirstName = "Norman", LastName = "Rockwell" },
            new Illustrator { Id = 5, FirstName = "Gustave", LastName = "Doré" },
            new Illustrator { Id = 6, FirstName = "Beya", LastName = "Rebaï" }
        );
    }

}