using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public class BookFacade : IBookFacade
    {
        private readonly IBookRepository bookRepository;

        public BookFacade(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public bool AddBooksToAuthor(params BookDto[] booksDtos)
        {
            var books = new List<BookDto>();

            foreach (var book in books)
            {
                var book = new Book
                {

                };
            }

            return result;
        }

        public bool GetAllBooks()
        {
            var result = bookRepository.GetAllBooks();

            return result;
        }

        public BookDto GetBookById(int id)
        {
            var book = bookRepository.GetBookById(id);

            var bookDto = new BookDto
            {
                BookId = book.BookId,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                Title = book.Title
            };

            return bookDto;
        }
    }
}
