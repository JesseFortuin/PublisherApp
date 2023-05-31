using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IBookFacade
    {
        public BookDto GetBookById(int id);

        public BookDto GetBookByTitle(string bookTitle);

        public List<BookDto> GetAllBooks();

        public bool AddManyBooks(params AddBookDto[] books);

        public List<BookAndCoverDto> GetAllBooksWithCovers();
    }
}
