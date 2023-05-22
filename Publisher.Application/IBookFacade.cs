using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IBookFacade
    {
        public BookDto GetBookById(int id);

        public bool GetAllBooks();

        public bool AddBooksToAuthor(params BookDto[] books);
    }
}
