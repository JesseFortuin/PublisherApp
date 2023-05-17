using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IPublisherFacade
    {
        public void AddAuthor(AddAuthorDto authorDto);

        public void GetAuthors();

        public void AddAuthorWithBook(AddAuthorDto authorDto, AddBookDto bookDto);

        public void GetAuthorsWithBooks();

        public void QueryFilters();

        public void FindAndPaginationQuery();

        public void AddCoupleOfAuthors(AddAuthorDto authorDto1, AddAuthorDto authorDto2);

        public void RetrieveAndUpdateMultipleAuthorsLastNames(string lastName, string updatedLastName);

        public void SkipAndTakeAuthors(int groupSize);

        public void SortAuthorsDecendingOrder();

        public void QueryAggregate(string lastName);

        public void InsertAuthor(AddAuthorDto authorDto);

        public void RetrieveAndUpdateAuthor(string name, string newName);

        public void CoordinatedRetrieveAndUpdateAuthor(int authorId, string originalName, string updatedName);

        public void DeleteAuthor(int authorId);

        public void InsertMultipleAuthors();
    }
}