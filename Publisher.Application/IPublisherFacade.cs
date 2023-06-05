using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IPublisherFacade
    {
        public ApiResponseDto<bool> AddAuthor(AddAuthorDto authorDto);

        public ApiResponseDto<List<AuthorDto>> GetAuthors();

        public ApiResponseDto<List<AuthorDto>> GetAuthorsWithSqlRaw();

        public ApiResponseDto<bool> AddAuthorWithBook(AddAuthorWithBookDto authorWithBookDto);

        public ApiResponseDto<bool> AddNewBookToExistingAuthor(string authorsLastName, AddAuthorBookDto bookDto);

        public ApiResponseDto<List<AuthorWithBooksDto>> GetAuthorsWithBooks();

        public ApiResponseDto<AuthorDto> GetAuthorById(int authorId);

        public ApiResponseDto<AuthorDto> GetAuthorByName(string firstName);

        public ApiResponseDto<List<AuthorDto>> GetAuthorsByRecentBook(int publishedOnAndAfter);

        public ApiResponseDto<bool> EagerLoadBooksWithAuthors();

        public ApiResponseDto<bool> SetBookBasePrice(int authorId, int bookId, decimal price);

        public void QueryFilters();

        public ApiResponseDto<bool> AddManyAuthors(params AddAuthorDto[] authorDtos);

        public ApiResponseDto<bool> RetrieveAndUpdateMultipleAuthorsLastNames(string lastName, string updatedLastName);

        public ApiResponseDto<bool> SkipAndTakeAuthors(int groupSize);

        public ApiResponseDto<bool> SortAuthorsDecendingOrder();

        public ApiResponseDto<bool> QueryAggregate(string lastName);

        public ApiResponseDto<bool> InsertAuthor(AddAuthorDto authorDto);

        public ApiResponseDto<bool> RetrieveAndUpdateAuthor(string name, string newName);

        public ApiResponseDto<bool> CoordinatedRetrieveAndUpdateAuthor(int authorId, string originalName, string updatedName);

        public ApiResponseDto<bool> DeleteAuthor(int authorId);

        public void InsertMultipleAuthors();
    }
}