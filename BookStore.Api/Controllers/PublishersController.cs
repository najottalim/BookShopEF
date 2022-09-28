using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Publishers;
using BookStore.Service.DTOs.Publishers;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/publishers")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublishersController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PublisherViewDto?>> Get(int id)
    {
        return Ok(await _publisherService.GetAsync(publisher => publisher.Id == id));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publisher>>> GetAll([FromQuery] string? search = null, [FromQuery] PaginationParameters? parameters = null)
    {
        return Ok(await _publisherService.GetAllAsync(string.IsNullOrEmpty(search)
                ? null
                : publisher => publisher.Name.ToLower().Contains(search.ToLower()),
            parameters));
    }

    [HttpPost]
    public async Task<ActionResult<Publisher>> Create(PublisherForCreationDto dto)
    {
        return Ok(await _publisherService.CreateAsync(dto));
    }
}