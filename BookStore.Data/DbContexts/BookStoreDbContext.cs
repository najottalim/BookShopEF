using BookStore.Domain.Entites.Books;
using BookStore.Domain.Entites.Orders;
using BookStore.Domain.Entites.Publishers;
using BookStore.Domain.Entites.Users;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.DbContexts;

public class BookStoreDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;User Id=postgres; Database=book_store; password=root");
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetails> OrderDetails { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    
}