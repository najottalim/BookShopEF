using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Enums;

namespace BookStore.Service.DTOs.Users;

public class UserForCreationDto
{
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [Phone, MaxLength(9)]
    public string Phone { get; set; }
    
    public string Password { get; set; }
}