using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Domain.Entites.Books;

namespace BookStore.Data.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
    }
}