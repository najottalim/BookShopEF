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
        optionsBuilder.UseNpgsql("Host=ec2-3-229-165-146.compute-1.amazonaws.com;Port=5432;User Id=plmihpdozeungo; " +
                                 "Database=d8odras2nmeu37; password=4bf3f2cc30450ecc4e736c722a0d0f35a41de3b5e8db6151a4f2a92ebb53075a");
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetails> OrderDetails { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    
}