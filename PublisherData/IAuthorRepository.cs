using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool SkipAndTakeAuthors(int groupSize);

        public bool SortAuthors();
    }
}
