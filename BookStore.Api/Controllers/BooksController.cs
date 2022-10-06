using System.Net;
using BookStore.Data.DbContexts;
using BookStore.Domain.Entites.Books;
using BookStore.Service.DTOs.Books;
using BookStore.Service.Exceptions;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id:int}"), Authorize]
    public async Task<ActionResult<Book?>> Get([FromRoute]int id)
    {
        return Ok(await _bookService.GetAsync(book => book.Id == id));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll([FromQuery]int? year, [FromQuery] int? publisherId)
    {
        if (year is null && publisherId is not null)
            return Ok(await _bookService.GetAllAsync(book => book.PublisherId == publisherId));

        if(publisherId is null && year is not null)
            return Ok(await _bookService.GetAllAsync(book => book.PublishYear == year));

        if (publisherId is not null && year is not null)
            return Ok(await _bookService.GetAllAsync(book => book.PublisherId == publisherId && book.PublishYear == year));
        
        return Ok(await _bookService.GetAllAsync());
    }

    [HttpPost, Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<Book>> Create(BookForCreationDto bookForCreationDto)
    {
        return Ok(await _bookService.CreateAsync(bookForCreationDto));
    }

    [HttpDelete, Authorize]
    public async Task<ActionResult<bool>> Delete([FromQuery]int? id, [FromQuery] int? year)
    {
        if (id is null && year is null || id is not null && year is not null)
            throw new HttpStatusCodeException(HttpStatusCode.BadRequest,
                "Need to provide only one parameter to delete");
        
        return id is not null
            ? Ok(await _bookService.DeleteAsync(book => book.Id == id))
            : Ok(await _bookService.DeleteAsync(book => book.PublishYear == year));
    }
    
    [HttpPatch]
    public async Task<ActionResult<Book>> Update(int id, BookForUpdateDto bookForUpdateDto)
    {
        return Ok(await _bookService.UpdateAsync(id, bookForUpdateDto));
    }
}