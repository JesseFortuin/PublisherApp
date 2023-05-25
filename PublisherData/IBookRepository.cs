using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface IBookRepository
    {
        public Book GetBookById(int id);

        public Book GetBookByTitle(string bookTitle);

        public List<Book> GetAllBooks();

        public bool AddManyBooks(params Book[] books);
    }
}
