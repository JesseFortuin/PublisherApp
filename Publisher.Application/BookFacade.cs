using Microsoft.IdentityModel.Tokens;
using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public class BookFacade : IBookFacade
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public BookFacade(IBookRepository bookRepository,
                          IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public bool AddManyBooks(params AddBookDto[] booksDto)
        {
            var books = new List<Book>();

            foreach (var bookDto in booksDto)
            {
                var author = authorRepository.FindAnAuthorById(bookDto.AuthorId);

                if (author == null)
                {
                    throw new Exception("Author Id does not match author in database");
                }

                var book = new Book
                {
                    Title = bookDto.Title,
                    PublishDate = bookDto.PublishDate,
                    AuthorId = author.AuthorId
                };

                books.Add(book);
            }

            var result = bookRepository.AddManyBooks(books.ToArray());

            return result;
        }

        public List<BookDto> GetAllBooks()
        {
            var Books = bookRepository.GetAllBooks();
            
            var BookDtos = new List<BookDto>();
            
            foreach (var book in Books)
            {
                var bookDto = new BookDto
                {
                    BookId = book.BookId,
                    AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                    Title = book.Title
                };

                BookDtos.Add(bookDto);
            }

            return BookDtos;
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
