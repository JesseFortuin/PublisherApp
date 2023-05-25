using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IPublisherFacade
    {
        public bool AddAuthor(AddAuthorDto authorDto);

        public List<AuthorDto> GetAuthors();

        public bool AddAuthorWithBook(AddAuthorDto authorDto, AddAuthorBookDto bookDto);

        public bool AddNewBookToExistingAuthor(string authorsLastName, AddAuthorBookDto bookDto);

        public List<AuthorDto> GetAuthorsWithBooks();

        public AuthorDto GetAuthorById(int authorId);

        public AuthorDto GetAuthorByName(string firstName);

        public bool EagerLoadBooksWithAuthors();

        public void QueryFilters();

        public bool AddManyAuthors(params AddAuthorDto[] authorDtos);

        public bool RetrieveAndUpdateMultipleAuthorsLastNames(string lastName, string updatedLastName);

        public bool SkipAndTakeAuthors(int groupSize);

        public bool SortAuthorsDecendingOrder();

        public bool QueryAggregate(string lastName);

        public bool InsertAuthor(AddAuthorDto authorDto);

        public bool RetrieveAndUpdateAuthor(string name, string newName);

        public void CoordinatedRetrieveAndUpdateAuthor(int authorId, string originalName, string updatedName);

        public bool DeleteAuthor(int authorId);

        public void InsertMultipleAuthors();
    }
}