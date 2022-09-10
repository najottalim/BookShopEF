using AutoMapper;
using BookStore.Domain.Entites.Books;
using BookStore.Service.DTOs.Books;

namespace BookStore.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Book, BookForCreationDto>().ReverseMap();
        CreateMap<Book, BookForUpdateDto>().ReverseMap();
    }
}