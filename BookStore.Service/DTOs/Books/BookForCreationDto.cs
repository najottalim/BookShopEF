using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Enums;

namespace BookStore.Service.DTOs.Books;

public class BookForCreationDto
{
    [MaxLength(50), MinLength(4), Required]
    public string Title { get; set; }
    
    [Required]
    public int PublishYear { get; set; }

    [Required]
    public int NumberOfPages { get; set; }
    
    [Required]
    public int PublisherId { get; set; }
    
    [Required]
    public int Price { get; set; }
    
    [Required]
    public Genre Genre { get; set; }
    
    [Required]
    public Language Language { get; set; }
}