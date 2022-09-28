using System.Linq.Expressions;
using System.Net;
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Data.Repositories;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Publishers;
using BookStore.Domain.Enums;
using BookStore.Service.DTOs.Publishers;
using BookStore.Service.Exceptions;
using BookStore.Service.Extensions;
using BookStore.Service.Interfaces;

namespace BookStore.Service.Services;

public class PublisherService : IPublisherService
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IBookRepository _bookRepository;

    public PublisherService(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
        _bookRepository = new BookRepository(dbContext);
        _publisherRepository = new PublisherRepository(dbContext);
    }

    public async Task<Publisher> CreateAsync(PublisherForCreationDto dto)
    {
        var existPublisher =
            await _publisherRepository.GetAsync(publisher => publisher.Name.ToLower().Equals(dto.Name.ToLower()));

        if (existPublisher is not null)
            throw new HttpStatusCodeException(HttpStatusCode.Conflict, "Publisher Already exists!");

        existPublisher = new Publisher()
        {
            CreatedAt = DateTime.UtcNow, Name = dto.Name, State = ItemState.Created
        };

        existPublisher = await _publisherRepository.CreateAsync(existPublisher);
        await _dbContext.SaveChangesAsync();

        return existPublisher;
    }

    public async Task<PublisherViewDto?> GetAsync(Expression<Func<Publisher, bool>> expression)
    {
        var publisher = await _publisherRepository.GetAsync(expression);

        return publisher is null
            ? null
            : new PublisherViewDto
            {
                Id = publisher.Id, Name = publisher.Name,
                Books = _bookRepository.GetAll(book => book.PublisherId == publisher.Id, false)
            };
    }

    public Task<IEnumerable<Publisher>> GetAllAsync(Expression<Func<Publisher, bool>>? expression = null,
        PaginationParameters? parameters = null)
    {
        return Task.FromResult(_publisherRepository.GetAll(expression).ToPaged(parameters));
    }
}