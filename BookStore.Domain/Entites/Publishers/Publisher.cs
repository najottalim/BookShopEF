using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BookStore.Domain.Commons;
using BookStore.Domain.Enums;


namespace BookStore.Domain.Entites.Publishers;

public class Publisher : Auditable, ILocalizationName
{
    [MaxLength(100), JsonIgnore] public string NameUz { get; set; } = null!;

    [MaxLength(100), JsonIgnore] public string NameRu { get; set; } = null!;

    [MaxLength(100), JsonIgnore] public string NameEn { get; set; } = null!;

    [NotMapped] public string Name { get; set; } = null!;

    public string GetLocalizationName(Localization localization = Localization.Uz)
    {
        return localization switch
        {
            Localization.Ru => NameRu,
            Localization.En => NameEn,
            _ => NameUz
        };
    }

    public Publisher SetLocalization(Localization localization = Localization.Uz)
    {
        Name = localization switch
        {
            Localization.Ru => NameRu,
            Localization.En => NameEn,
            _ => NameUz
        };

        return this;
    }
}