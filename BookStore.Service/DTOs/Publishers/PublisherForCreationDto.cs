using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Commons;

namespace BookStore.Service.DTOs.Publishers;

public class PublisherForCreationDto : ILocalizationName
{
    [Required]
    [MaxLength(100)]
    public string NameUz { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string NameRu { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string NameEn { get; set; } = null!;
}