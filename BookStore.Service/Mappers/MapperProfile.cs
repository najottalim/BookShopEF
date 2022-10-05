using AutoMapper;
using BookStore.Domain.Entites.Books;
using BookStore.Domain.Entites.Users;
using BookStore.Service.DTOs.Books;
using BookStore.Service.DTOs.Users;

namespace BookStore.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Book, BookForCreationDto>().ReverseMap();
        CreateMap<Book, BookForUpdateDto>().ReverseMap();

        CreateMap<UserForCreationDto, User>();
    }
}