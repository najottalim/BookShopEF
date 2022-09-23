using System.Linq.Expressions;
using AutoMapper;
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Data.Repositories;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Books;
using BookStore.Domain.Entites.Publishers;
using BookStore.Domain.Enums;
using BookStore.Service.DTOs.Books;
using BookStore.Service.Extensions;
using BookStore.Service.Interfaces;
using BookStore.Service.Mappers;

namespace BookStore.Service.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IGenericRepository<Publisher> _publisherRepository;
    
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public BookService(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _bookRepository = new BookRepository(_dbContext);
        _publisherRepository = new GenericRepository<Publisher>(_dbContext);

        _mapper = mapper;
    }
    
    public async Task<Book> CreateAsync(BookForCreationDto dto)
    {
        var publisher = await _publisherRepository.GetAsync(publisher => publisher.Id == dto.PublisherId);
        if (publisher is null)
            throw new Exception("Publisher not found");

        var mapped = _mapper.Map<Book>(dto);
        mapped.Isbn = Guid.NewGuid();
        mapped.CreatedAt = DateTime.UtcNow;

        var book = await _bookRepository.CreateAsync(mapped);
        await _dbContext.SaveChangesAsync();

        return book;
    }

    public async Task<Book> UpdateAsync(int id, BookForUpdateDto dto)
    {
        var book = await _bookRepository.GetAsync(book => book.Id == id && book.State != ItemState.Deleted);

        if (book is null)
            throw new Exception("Book not found!");

        if (!string.IsNullOrEmpty(dto.Title)) 
            book.Title = dto.Title;

        if (dto.Price is not null)
            book.Price = (int)dto.Price;

        if (dto.PublishYear is not null)
            book.PublishYear = (int) dto.PublishYear;

        if (dto.NumberOfPages is not null)
            book.NumberOfPages = (int) dto.NumberOfPages;

        book.UpdatedAt = DateTime.UtcNow;
        book.State = ItemState.Updated;
        book = await _bookRepository.UpdateAsync(book);
        await _dbContext.SaveChangesAsync();
        
        return book;
    }
    
    public async Task<bool> DeleteAsync(Expression<Func<Book, bool>> expression)
    {
        var books = _bookRepository.GetAll(expression).Where(p => p.State != ItemState.Deleted);

        if (!books.Any())
            throw new Exception("Book not found!");

        _bookRepository.DeleteRange(books);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public Task<Book?> GetAsync(Expression<Func<Book, bool>> expression)
        => _bookRepository.GetAsync(expression);

    public Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null,
        PaginationParameters? parameters = null)
    {
        return Task.FromResult(_bookRepository.GetAll(expression, false)
            .Where(p => p.State != ItemState.Deleted)
            .ToPaged(parameters));
    }
}