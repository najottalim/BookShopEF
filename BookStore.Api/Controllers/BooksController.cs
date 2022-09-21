using BookStore.Domain.Entites.Books;
using BookStore.Service.DTOs.Books;
using BookStore.Service.Interfaces;
using BookStore.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController, Route("api/books")]
public class BooksController
{
    private readonly IBookService _bookService;

    public BooksController()
    {
        _bookService = new BookService();
    }
    
    [HttpGet]
    public async Task<IEnumerable<Book>> GetAll()
    {
        return await _bookService.GetAllAsync();
    }
    
    [HttpGet("year")]
    public async Task<IEnumerable<Book>> GetAllByYear(int year)
    {
        return await _bookService.GetAllAsync(book => book.PublishYear == year);
    }
    
    [HttpGet("publisher")]
    public async Task<IEnumerable<Book>> GetAllByPublisher(int publisherId)
    {
        return await _bookService.GetAllAsync(book => book.PublisherId == publisherId);
    }

    [HttpPost]
    public async Task<Book> Create(BookForCreationDto bookForCreationDto)
    {
        return await _bookService.CreateAsync(bookForCreationDto);
    }

    [HttpDelete]
    public async Task<bool> Delete(int id)
    {
        return await _bookService.DeleteAsync(book => book.Id == id);
    }

    [HttpDelete("by-year")]
    public async Task<bool> DeleteByYear(int year)
    {
        return await _bookService.DeleteAsync(book => book.PublishYear == year);
    }

    [HttpPatch]
    public async Task<Book> Update(int id, BookForUpdateDto bookForUpdateDto)
    {
        return await _bookService.UpdateAsync(id, bookForUpdateDto);
    }
}