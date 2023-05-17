using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface IAuthorRepository
    {
        public bool AddAuthor(Author author);

        public bool AddAuthorWithBook(Author author);

        public bool AddSomeMoreAuthors(Author author1, Author author2);

        public bool GetAuthors();

        public bool GetAuthorsWithBooks();

        public bool InsertAuthor(Author author);

        public bool QueryAggregate(string lastName);

        public bool QueryFilters(string name, string filters);

        public bool RetrieveAndUpdateAuthor(string name, string newName);

        public bool RetrieveAndUpdateMultipleAuthorsLastNames(string name, string updatedLastName);

        public bool SkipAndTakeAuthors(int groupSize);

        public bool SortAuthors();

        public Author FindAnAuthor(int authorId);

        public bool SaveAnAuthor(Author author);

        public bool DeleteAnAuthor(int authorId);

        public bool InsertMultipleAuthors();
    }
}
