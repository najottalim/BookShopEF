using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Commons;
using BookStore.Domain.Enums;

namespace BookStore.Domain.Entites.Users;

public class User : Auditable
{
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    [Phone, MaxLength(9)]
    public string Phone { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public UserRole UserRole { get; set; }
}