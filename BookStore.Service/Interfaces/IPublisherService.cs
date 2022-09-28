using System.Linq.Expressions;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Publishers;
using BookStore.Service.DTOs.Publishers;

namespace BookStore.Service.Interfaces;

public interface IPublisherService
{
    Task<Publisher> CreateAsync(PublisherForCreationDto dto);
    
    Task<PublisherViewDto?> GetAsync(Expression<Func<Publisher, bool>> expression);

    Task<IEnumerable<Publisher>> GetAllAsync(Expression<Func<Publisher, bool>>? expression = null,
        PaginationParameters? parameters = null);

}