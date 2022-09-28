using System.ComponentModel.DataAnnotations;

namespace BookStore.Service.DTOs.Publishers;

public class PublisherForCreationDto
{
    [Required, MaxLength(100), MinLength(8)]
    public string Name { get; set; }
}