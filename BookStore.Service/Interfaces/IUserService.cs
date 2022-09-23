using System.Linq.Expressions;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Users;
using BookStore.Service.DTOs.Users;

namespace BookStore.Service.Interfaces;

public interface IUserService
{
    Task<User> CreateAsync(UserForCreationDto dto);
    Task<User> UpdateAsync(int id, UserForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    Task<User?> GetAsync(Expression<Func<User, bool>> expression);

    Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null,
        PaginationParameters? parameters = null);
}