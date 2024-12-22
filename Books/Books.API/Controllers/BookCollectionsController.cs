using AutoMapper;
using Books.API.Filters;
using Books.API.Models;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[Route("api/bookcollections")]
[ApiController]
[TypeFilter(typeof(BooksResultFilter))]
public class BookCollectionsController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;

    public BookCollectionsController(IBooksRepository booksRepository,
        IMapper mapper)
    {
        _booksRepository = booksRepository ??
            throw new ArgumentNullException(nameof(booksRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookCollection(
         IEnumerable<BookForCreationDto> bookCollection)
    {
        var bookEntities = _mapper.Map<IEnumerable<Entities.Book>>(bookCollection);
        foreach (var bookEntity in bookEntities)
        {
            _booksRepository.AddBook(bookEntity);
        }

        await _booksRepository.SaveChangesAsync();

        return Ok();
    }
}