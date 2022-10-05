using BookStore.Domain.Commons;

namespace BookStore.Service.DTOs.Books;

public class BookForUpdateDto : ILocalizationName
{
    public int? PublishYear { get; set; }

    public int? NumberOfPages { get; set; }
    
    public int? Price { get; set; }
    
    public string? NameUz { get; set; }
    public string? NameRu { get; set; }
    public string? NameEn { get; set; }
}