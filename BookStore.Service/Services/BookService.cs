using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Data.Repositories;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Books;
using BookStore.Domain.Enums;
using BookStore.Service.DTOs.Books;
using BookStore.Service.Exceptions;
using BookStore.Service.Extensions;
using BookStore.Service.Helpers;
using BookStore.Service.Interfaces;

namespace BookStore.Service.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IPublisherRepository _publisherRepository;
    
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public BookService(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _bookRepository = new BookRepository(_dbContext);
        _publisherRepository = new PublisherRepository(_dbContext);

        _mapper = mapper;
    }
    
    public async Task<Book> CreateAsync(BookForCreationDto dto)
    {
        var publisher = await _publisherRepository.GetAsync(publisher => publisher.Id == dto.PublisherId);
        if (publisher is null)
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Publisher not found");

        var mapped = _mapper.Map<Book>(dto);
        mapped.Isbn = Guid.NewGuid();
        mapped.CreatedAt = DateTime.UtcNow;

        var book = await _bookRepository.CreateAsync(mapped);
        await _dbContext.SaveChangesAsync();

        return book.SetLocalization(HttpContextHelper.Localization);
    }

    public async Task<Book> UpdateAsync(int id, BookForUpdateDto dto)
    {
        var book = await _bookRepository.GetAsync(book => book.Id == id && book.State != ItemState.Deleted);

        if (book is null)
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Book not found!");

        if (!string.IsNullOrEmpty(dto.NameUz)) 
            book.NameUz = dto.NameUz;
        
        if (!string.IsNullOrEmpty(dto.NameRu)) 
            book.NameRu = dto.NameRu;

        if (!string.IsNullOrEmpty(dto.NameEn))
            book.NameEn = dto.NameEn;
        
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
        
        return book.SetLocalization(HttpContextHelper.Localization);
    }
    
    public async Task<bool> DeleteAsync(Expression<Func<Book, bool>> expression)
    {
        var books = _bookRepository.GetAll(expression).Where(p => p.State != ItemState.Deleted);

        if (!books.Any())
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Book not found!");

        _bookRepository.DeleteRange(books);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<Book?> GetAsync(Expression<Func<Book, bool>> expression)
    {
        var book = await _bookRepository.GetAsync(expression);

        return book == null || book.State == ItemState.Deleted
            ? null
            : book.SetLocalization(HttpContextHelper.Localization);
    }

    public Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null,
        PaginationParameters? parameters = null)
    {
        return Task.FromResult(_bookRepository
            .GetAll(expression, false)
            .Where(p => p.State != ItemState.Deleted)
            .ToPagedAsEnumerable(parameters)
            .Select(book => book.SetLocalization(HttpContextHelper.Localization))
        );
    }
}