using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Domain.Entites.Publishers;

namespace BookStore.Data.Repositories;

public class PublisherRepository: GenericRepository<Publisher>, IPublisherRepository
{
    public PublisherRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
    }
}