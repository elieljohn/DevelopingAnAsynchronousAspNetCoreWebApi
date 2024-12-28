using Books.API.DbContexts;
using Books.API.Entities;
using Books.API.Models.External;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Books.API.Services;

public class BooksRepository : IBooksRepository
{
    private readonly BooksContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public BooksRepository(BooksContext context,
        IHttpClientFactory httpClientFactory)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
        _httpClientFactory = httpClientFactory ??
            throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public void AddBook(Book bookToAdd)
    {
        if (bookToAdd == null)
        {
            throw new ArgumentNullException(nameof(bookToAdd));
        }

        _context.Add(bookToAdd);
    }

    public async Task<Book?> GetBookAsync(Guid id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<BookCoverDto?> GetBookCoverAsync(string id)
    {
        var httpClient = _httpClientFactory.CreateClient();

        // pass through a dummy name
        var response = await httpClient
               .GetAsync($"http://localhost:52644/api/bookcovers/{id}");
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<Models.External.BookCoverDto>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
        }

        return null;
    }

    public IEnumerable<Book> GetBooks()
    {
        return _context.Books
            .Include(b => b.Author)
            .ToList();
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Author)
            .ToListAsync();
    }

    public IAsyncEnumerable<Book> GetBooksAsAsyncEnumerable()
    {
        return _context.Books.AsAsyncEnumerable<Book>();
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds)
    {
        return await _context.Books
            .Where(b => bookIds.Contains(b.Id))
            .Include(b => b.Author)
            .ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        // return true if 1 or more entities were changed
        return (await _context.SaveChangesAsync() > 0);
    }
}
