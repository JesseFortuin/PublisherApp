using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IBookFacade
    {
        public ApiResponseDto<BookDto> GetBookById(int id);

        public ApiResponseDto<BookDto> GetBookByTitle(string bookTitle);

        public ApiResponseDto<List<BookDto>> GetAllBooks();

        public ApiResponseDto<bool> AddManyBooks(params AddBookDto[] books);

        public ApiResponseDto<List<BookAndCoverDto>> GetAllBooksWithCovers();

        public ApiResponseDto<bool> AddBookWithCover(AddBookWithCoverDto bookWithCover);
    }
}
