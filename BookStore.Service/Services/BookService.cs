using System.Linq.Expressions;
using AutoMapper;
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Data.Repositories;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Books;
using BookStore.Service.DTOs.Books;
using BookStore.Service.Extensions;
using BookStore.Service.Interfaces;
using BookStore.Service.Mappers;

namespace BookStore.Service.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public BookService()
    {
        _dbContext = new BookStoreDbContext();
        _bookRepository = new BookRepository(_dbContext);
        
        _mapper = new Mapper(new MapperConfiguration(c =>
        {
            c.AddProfile<MapperProfile>();
        }));
    }
    
    public async Task<Book> CreateAsync(BookForCreationDto dto)
    {
        var book = await _bookRepository.CreateAsync(_mapper.Map<Book>(dto));

        await _dbContext.SaveChangesAsync();

        return book;
    }

    public async Task<Book> UpdateAsync(int id, BookForUpdateDto dto)
    {
        var book = await _bookRepository.GetAsync(book => book.Id == id);

        if (book is null)
        {
            throw new Exception("Book not found!");
        }

        if (!string.IsNullOrEmpty(dto.Title)) 
            book.Title = dto.Title;

        if (dto.Price is not null)
            book.Price = (int)dto.Price;

        if (dto.PublishYear is not null)
            book.PublishYear = (int) dto.PublishYear;

        if (dto.NumberOfPages is not null)
            book.NumberOfPages = (int) dto.NumberOfPages;

        book = await _bookRepository.UpdateAsync(book);
        await _dbContext.SaveChangesAsync();
        
        return book;
    }
    
    public async Task<bool> DeleteAsync(Expression<Func<Book, bool>> expression)
    {
        var books = _bookRepository.GetAll(expression);

        if (!books.Any())
        {
            throw new Exception("Book not found!");
        }

        _bookRepository.DeleteRange(books);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public Task<Book?> GetAsync(Expression<Func<Book, bool>> expression)
        => _bookRepository.GetAsync(expression);

    public Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null,
        PaginationParameters? parameters = null)
        => Task.FromResult(_bookRepository.GetAll(expression).ToPaged(parameters));
}