namespace BookStore.Service.DTOs.Books;

public class BookForUpdateDto
{
    public string? Title { get; set; }
    
    public int? PublishYear { get; set; }

    public int? NumberOfPages { get; set; }
    
    public int? Price { get; set; }
}