using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;

namespace Publisher.Infrastructure
{
    public interface IAuthorRepository
    {
        public bool AddAuthor(Author author);

        public bool AddAuthorWithBook(Author author);

        public bool AddNewBookToExistingAuthor(string authorsLastName, AddAuthorBookDto bookDto);

        public bool AddManyAuthors(params Author[] authors);

        public bool EagerLoadBooksWithAuthors();

        public bool ExplicitLoadCollection();

        public List<Author> GetAuthors();

        public Author GetAuthorByName(string name);

        public List<Author> GetAuthorsWithBooks();

        public bool InsertAuthor(Author author);

        public bool QueryAggregate(string lastName);

        public bool QueryFilters(string name, string filters);

        public bool RetrieveAndUpdateAuthor(string name, string newName);

        public bool RetrieveAndUpdateMultipleAuthorsLastNames(string name, string updatedLastName);

        public bool SkipAndTakeAuthors(int groupSize);

        public bool SortAuthors();

        //public bool FindAuthor(int authorId);

        public Author FindAnAuthorById(int authorId);

        public bool SaveAnAuthor(Author author);

        public bool DeleteAnAuthor(int authorId);

        public bool InsertMultipleAuthors();
    }
}
