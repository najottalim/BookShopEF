using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Commons;
using BookStore.Domain.Enums;

namespace BookStore.Service.DTOs.Books;

public class BookForCreationDto : ILocalizationName
{
    [MaxLength(50)]
    [MinLength(4)]
    [Required]
    public string NameUz { get; set; } = null!;

    [MaxLength(50)]
    [MinLength(4)]
    [Required]
    public string NameRu { get; set; } = null!;

    [MaxLength(50)]
    [MinLength(4)]
    [Required]
    public string NameEn { get; set; } = null!;
    
    [Required] public int PublishYear { get; set; }

    [Required] public int NumberOfPages { get; set; }

    [Required] public int PublisherId { get; set; }

    [Required] public int Price { get; set; }

    [Required] public Genre Genre { get; set; }

    [Required] public Language Language { get; set; }
}