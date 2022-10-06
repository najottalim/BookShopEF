using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Data.Repositories;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Users;
using BookStore.Domain.Enums;
using BookStore.Service.DTOs.Users;
using BookStore.Service.Exceptions;
using BookStore.Service.Extensions;
using BookStore.Service.Interfaces;

namespace BookStore.Service.Services;

public class UserService : IUserService
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAuthManager _authManager;
    private readonly IGenericRepository<User> _userRepository;
    
    public UserService(BookStoreDbContext dbContext, IMapper mapper, IAuthManager authManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _authManager = authManager;
        _userRepository = new GenericRepository<User>(dbContext);
    }

    public async Task<string> LoginAsync(string login, string password)
    {
        var user = await _userRepository.GetAsync(user => user.Phone.Equals(login));

        if (user is null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No such user");

        if (!user.Password.Equals(password))
            throw new HttpStatusCodeException(HttpStatusCode.BadRequest,
                "Wrong password");

        return _authManager.CreateToken(user);
    }

    public async Task<User> CreateAsync(UserForCreationDto dto, UserRole role = UserRole.Customer)
    {
        var user = _mapper.Map<User>(dto);
        user.UserRole = role;
        user.CreatedAt = DateTime.UtcNow;
        
        user = await _userRepository.CreateAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public Task<User> UpdateAsync(int id, UserForUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetAsync(Expression<Func<User, bool>> expression)
    {
        return _userRepository.GetAsync(expression);
    }

    public Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null, PaginationParameters? parameters = null)
    {
        return Task.FromResult<IEnumerable<User>>(_userRepository.GetAll(expression, false)
            .ToPagedAsQueryable(parameters));
    }
}