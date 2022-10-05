using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BookStore.Domain.Commons;
using BookStore.Domain.Entites.Publishers;
using BookStore.Domain.Enums;

namespace BookStore.Domain.Entites.Books;

public class Book : Auditable, ILocalizationName
{
    [MaxLength(50), JsonIgnore] public string NameUz { get; set; } = null!;
    [MaxLength(50), JsonIgnore] public string NameRu { get; set; } = null!;
    [MaxLength(50), JsonIgnore] public string NameEn { get; set; } = null!;

    [NotMapped] public string Name { get; set; } = null!;
    
    public int PublishYear { get; set; }
    public int NumberOfPages { get; set; }
    
    public int PublisherId { get; set; }
    [ForeignKey(nameof(PublisherId))] 
    public Publisher? Publisher { get; set; }
    
    public Guid Isbn { get; set; }
    public int Price { get; set; }
    
    public Genre Genre { get; set; }
    public Language Language { get; set; }

    public Book SetLocalization(Localization localization = Localization.Uz)
    {
        Name = localization switch
        {
            Localization.Ru => NameRu,
            Localization.En => NameEn,
            _ => NameUz
        };

        return this;
    }

    public string GetLocalizationName(Localization localization = Localization.Uz)
    {
        return localization switch
        {
            Localization.Ru => NameRu,
            Localization.En => NameEn,
            _ => NameUz
        };
    }
    
}