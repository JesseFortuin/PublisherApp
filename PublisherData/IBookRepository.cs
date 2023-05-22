using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface IBookRepository
    {
        public Book GetBookById(int id);

        public bool GetAllBooks();

        public bool AddBooksToAuthor(params Book[] books);
    }
}
