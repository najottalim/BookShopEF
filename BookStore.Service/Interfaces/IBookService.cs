using System.Linq.Expressions;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Books;
using BookStore.Service.DTOs.Books;

namespace BookStore.Service.Interfaces;

public interface IBookService
{
    Task<Book> CreateAsync(BookForCreationDto dto);
    Task<Book> UpdateAsync(int id, BookForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<Book, bool>> expression);
    Task<Book?> GetAsync(Expression<Func<Book, bool>> expression);

    Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null,
        PaginationParameters? parameters = null);
}