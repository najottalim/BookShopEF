
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Data.Repositories;
using BookStore.Domain.Entites.Users;
using BookStore.Domain.Enums;

var dbContext = new BookStoreDbContext();

IGenericRepository<User> userRepository = new GenericRepository<User>(dbContext);

foreach (var user in userRepository.GetAll(p => p.Id > 1))
{
    Console.WriteLine($"{user.Id} {user.FirstName}");
}
