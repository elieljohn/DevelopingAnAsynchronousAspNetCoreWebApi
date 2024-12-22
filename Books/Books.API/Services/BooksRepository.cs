using Books.API.DbContexts;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Services;

public class BooksRepository : IBooksRepository
{
    private readonly BooksContext _context;

    public BooksRepository(BooksContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
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
