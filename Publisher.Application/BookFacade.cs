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

        public ApiResponseDto<bool> AddBookWithCover(AddBookWithCoverDto bookWithCover)
        {
            var book = new Book
            {
                AuthorId = 1,
                Title = bookWithCover.Title,
                PublishDate = bookWithCover.PublishDate,
            };

            var cover = new Cover
            {
                DesignIdeas = bookWithCover.DesignIdeas,
            };

            book.Cover = cover;

            var result = bookRepository.AddBook(book);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> AddManyBooks(params AddBookDto[] booksDto)
        {
            var books = new List<Book>();

            foreach (var bookDto in booksDto)
            {
                var author = authorRepository.FindAnAuthorById(bookDto.AuthorId);

                if (author == null)
                {
                    return new ApiResponseDto<bool>("Author Id does not match author in database");
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

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<List<BookDto>> GetAllBooks()
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

            return new ApiResponseDto<List<BookDto>>(BookDtos);
        }

        public ApiResponseDto<List<BookAndCoverDto>> GetAllBooksWithCovers()
        {
            var booksAndCoversDto = new List<BookAndCoverDto>();

            var bookAndCover = bookRepository.GetAllBooksWithCovers();

            foreach (var book in bookAndCover)
            {
                var bookAndCoverDto = new BookAndCoverDto
                {
                    Title = book.Title,
                    BookCover = book.Cover.DesignIdeas
                };

                booksAndCoversDto.Add(bookAndCoverDto);
            }

            return new ApiResponseDto<List<BookAndCoverDto>>(booksAndCoversDto);
        }

        public ApiResponseDto<BookDto> GetBookById(int id)
        {
            var book = bookRepository.GetBookById(id);

            var bookDto = new BookDto
            {
                BookId = book.BookId,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                Title = book.Title
            };

            return new ApiResponseDto<BookDto>(bookDto);
        }

        public ApiResponseDto<BookDto> GetBookByTitle(string bookTitle)
        {
            var book = bookRepository.GetBookByTitle(bookTitle);

            var bookDto = new BookDto
            {
                BookId = book.BookId,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                Title = book.Title
            };

            return new ApiResponseDto<BookDto>(bookDto);
        }
    }
}
