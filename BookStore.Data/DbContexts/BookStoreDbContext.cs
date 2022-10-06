using BookStore.Domain.Entites.Books;
using BookStore.Domain.Entites.Orders;
using BookStore.Domain.Entites.Publishers;
using BookStore.Domain.Entites.Users;
using BookStore.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.DbContexts;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            FirstName = "Jon", LastName = "Doe",
            UserRole = UserRole.SuperAdmin,
            Phone = "991234567", Password = "string",
            CreatedAt = DateTime.UtcNow, State = ItemState.Created
        });

        modelBuilder.Entity<Publisher>().HasData(new Publisher
        {
            Id = 1,
            NameUz = "Darakchi", NameRu = "Даракчи", NameEn = "Darakchi",
            CreatedAt = DateTime.UtcNow, State = ItemState.Created
        }, new Publisher
        {
            Id = 2,
            NameUz = "Xaql so'zi", NameRu = "Слова народа", NameEn = "Folk word",
            CreatedAt = DateTime.UtcNow, State = ItemState.Created
        });

        modelBuilder.Entity<Book>().HasData(new Book
        {
            Id = 1,
            NameUz = "Molxona", NameRu = "Сарай", NameEn = "Barn",
            Language = Language.Uz, Isbn = Guid.NewGuid(),
            Genre = Genre.Fantastic, Price = 27000, PublisherId = 1,
            PublishYear = 2020, NumberOfPages = 200,
            CreatedAt = DateTime.UtcNow, State = ItemState.Created
        }, new Book
        {
            Id = 2,
            NameUz = "Choli qushi", NameRu = "Старая птичка", NameEn = "Old bird",
            Language = Language.Ru, Isbn = Guid.NewGuid(),
            Genre = Genre.Drama, Price = 60000, PublisherId = 2,
            PublishYear = 2021, NumberOfPages = 240,
            CreatedAt = DateTime.UtcNow, State = ItemState.Created
        });
    }

    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Book> Books { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<OrderDetails> OrderDetails { get; set; } = null!;
    public virtual DbSet<Publisher> Publishers { get; set; } = null!;
    
}