using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Commons;
using BookStore.Domain.Enums;

namespace BookStore.Domain.Entites.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [Phone, MaxLength(9)]
    public string Phone { get; set; }
    
    public string Password { get; set; }
    
    public UserRole UserRole { get; set; }
}