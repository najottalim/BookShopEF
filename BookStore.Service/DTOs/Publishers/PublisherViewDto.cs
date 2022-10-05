using BookStore.Domain.Entites.Books;

namespace BookStore.Service.DTOs.Publishers;

public class PublisherViewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<Book>? Books { get; set; }
}