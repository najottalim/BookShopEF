using System.Linq.Expressions;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Users;
using BookStore.Domain.Enums;
using BookStore.Service.DTOs.Users;

namespace BookStore.Service.Interfaces;

public interface IUserService
{
    Task<string> LoginAsync(string login, string password);

    Task<User> CreateAsync(UserForCreationDto dto, UserRole userRole = UserRole.Customer);
    Task<User> UpdateAsync(int id, UserForUpdateDto dto);
    Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    Task<User?> GetAsync(Expression<Func<User, bool>> expression);

    Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null,
        PaginationParameters? parameters = null);
}