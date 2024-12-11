using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[Route("api")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;

    public BooksController(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository
            ?? throw new ArgumentNullException(nameof(booksRepository));
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetBooks()
    {
        var bookEntities = await _booksRepository.GetBooksAsync();
        return Ok(bookEntities);
    }

    [HttpGet("books/{id}")]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var bookEntity = await _booksRepository.GetBookAsync(id);
        if (bookEntity == null)
        {
            return NotFound();
        }

        return Ok(bookEntity);
    }

}
