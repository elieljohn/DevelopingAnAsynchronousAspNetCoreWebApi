namespace Books.API.Services;

public interface IBooksRepository
{
    IEnumerable<Entities.Book> GetBooks();
    //Entities.Book? GetBook(Guid id);
    Task<IEnumerable<Entities.Book>> GetBooksAsync(IEnumerable<Guid> bookIds);
    Task<IEnumerable<Entities.Book>> GetBooksAsync();
    IAsyncEnumerable<Entities.Book> GetBooksAsAsyncEnumerable();
    Task<Entities.Book?> GetBookAsync(Guid id);
    void AddBook(Entities.Book bookToAdd);
    Task<bool> SaveChangesAsync();
}
