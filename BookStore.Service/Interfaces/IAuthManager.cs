using BookStore.Domain.Entites.Users;

namespace BookStore.Service.Interfaces;

public interface IAuthManager
{
    string CreateToken(User user);
}