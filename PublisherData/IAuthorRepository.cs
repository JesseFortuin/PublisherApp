using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface IAuthorRepository
    {
        public bool AddAuthor(Author author);

        public bool AddAuthorWithBook(Author author);

        public bool AddManyAuthors(params Author[] authors);

        public bool EagerLoadBooksWithAuthors();

        public bool ExplicitLoadCollection();

        public List<Author> GetAuthors();

        public List<AuthorByArtist> GetAuthorsWithArtists();

        public Author GetAuthorByName(string name);

        public Author GetAuthorByLastName(string lastName);

        public List<Author> GetAuthorsWithBooks();

        public List<Author> GetAuthorsByRecentBook(int publishedOnAndAfter);

        public bool InsertAuthor(Author author);

        public bool QueryAggregate(string lastName);

        public bool QueryFilters(string name, string filters);

        public List<Author> RetrieveAuthorsByLastName(string name);

        public bool SkipAndTakeAuthors(int groupSize);

        public bool SortAuthors();

        //public bool FindAuthor(int authorId);

        public Author FindAnAuthorById(int authorId);

        public bool SaveAnAuthor(Author author);

        public bool DeleteAnAuthor(int authorId);

        public bool InsertMultipleAuthors();

        public Author GetAuthorByIdWithBooks(int authorId);

        public bool UpdateAuthorBook(Author author, int bookId);

        public bool CascadeDelete(int authorId);

        public List<Author> SimpleRawSql();
    }
}
