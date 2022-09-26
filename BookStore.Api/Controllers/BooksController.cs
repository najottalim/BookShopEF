using BookStore.Domain.Entites.Books;
using BookStore.Service.DTOs.Books;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController, Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{id:int}")]
    public async Task<Book?> Get([FromRoute]int id)
    {
        return await _bookService.GetAsync(book => book.Id == id);
    }
    
    [HttpGet]
    public async Task<IEnumerable<Book>> GetAll([FromQuery]int? year, [FromQuery] int? publisherId)
    {
        if (year is null && publisherId is not null)
            return await _bookService.GetAllAsync(book => book.PublisherId == publisherId);

        if(publisherId is null && year is not null)
            return await _bookService.GetAllAsync(book => book.PublishYear == year);

        if (publisherId is not null && year is not null)
            return await _bookService.GetAllAsync(book => book.PublisherId == publisherId && book.PublishYear == year);
        
        return await _bookService.GetAllAsync();
    }

    [HttpPost]
    public async Task<Book> Create(BookForCreationDto bookForCreationDto)
    {
        return await _bookService.CreateAsync(bookForCreationDto);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery]int? id, [FromQuery] int? year)
    {
        if (id is null && year is null)
            return BadRequest("Need to provide any parameter to delete");

        return id is not null
            ? Ok(await _bookService.DeleteAsync(book => book.Id == id))
            : Ok(await _bookService.DeleteAsync(book => book.PublishYear == year));
    }
    
    [HttpPatch]
    public async Task<Book> Update(int id, BookForUpdateDto bookForUpdateDto)
    {
        return await _bookService.UpdateAsync(id, bookForUpdateDto);
    }
}