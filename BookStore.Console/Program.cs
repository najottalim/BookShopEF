using System;
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Data.Repositories;
using BookStore.Domain.Entites.Publishers;
using BookStore.Domain.Enums;

var dbContext = new BookStoreDbContext();

IGenericRepository<Publisher> repository = new GenericRepository<Publisher>(dbContext);
//
// await repository.CreateAsync(new Publisher
// {
//     CreatedAt = DateTime.UtcNow, Name = "XalqSo'zi", State = ItemState.Created
// });
//
// await dbContext.SaveChangesAsync();

await repository.CreateAsync(new Publisher()
{
    CreatedAt = DateTime.UtcNow, Name = "Darakchi", State = ItemState.Created
});

await dbContext.SaveChangesAsync();
