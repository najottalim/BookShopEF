using System.ComponentModel.DataAnnotations;

namespace BookStore.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required] public string FirstName { get; set; } = null!;

    [Required] public string LastName { get; set; } = null!;

    [Phone] [MaxLength(9)] [Required] public string Phone { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}