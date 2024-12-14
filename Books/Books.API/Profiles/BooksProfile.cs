using AutoMapper;
using Books.API.Entities;
using Books.API.Models;

namespace Books.API.Profiles;

public class BooksProfile : Profile
{
    public BooksProfile()
    {
        CreateMap<Book, BookDto>()
         .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src =>
             $"{src.Author.FirstName} {src.Author.LastName}"))
         .ConstructUsing(src => new BookDto(src.Id,
             string.Empty,
             src.Title,
             src.Description));
    }
}
