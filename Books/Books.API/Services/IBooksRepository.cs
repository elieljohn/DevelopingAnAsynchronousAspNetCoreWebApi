namespace Books.API.Services;

public interface IBooksRepository
{
    IEnumerable<Entities.Book> GetBooks();
    //Entities.Book? GetBook(Guid id);

    Task<IEnumerable<Entities.Book>> GetBooksAsync();
    Task<Entities.Book?> GetBookAsync(Guid id);
}
