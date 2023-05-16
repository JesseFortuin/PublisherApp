using Microsoft.EntityFrameworkCore;
using Publisher.Domain.Entities;
using PublisherData;

namespace Publisher.Infrastructure
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly PubContext pubContext;

        public AuthorRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }

        public bool AddAuthor(Author author)
        {
            pubContext.Authors.Add(author);

            pubContext.SaveChanges();

            return true;
        }

        public bool AddAuthorWithBook(Author author)
        {
            pubContext.Authors.Add(author);

            pubContext.SaveChanges();

            return true;
        }

        public bool AddSomeMoreAuthors(Author author1, Author author2)
        {
            pubContext.Authors.Add(author1);

            pubContext.Authors.Add(author2);

            pubContext.SaveChanges();

            return true;
        }

        public bool FindAndPaginationQuery()
        {
            throw new NotImplementedException();
        }

        public bool GetAuthors()
        {
            using var pubContext = new PubContext();

            var authors = pubContext.Authors.ToList();

            foreach (var author in authors)
            {
                Console.WriteLine(author.FirstName + " " + author.LastName);
            }

            return true;
        }

        public bool GetAuthorsWithBooks()
        {
            using var context = new PubContext();

            var authors = context.Authors.Include(a => a.Books).ToList();

            foreach (var author in authors)
            {
                Console.WriteLine(author.FirstName + " " + author.LastName);

                foreach (var book in author.Books)
                {
                    Console.WriteLine(book.Title);
                }
            }

            return true;
        }

        public bool InsertAuthor(Author author)
        {
            pubContext.Authors.Add(author);

            pubContext.SaveChanges();

            return true;
        }

        public bool QueryAggregate(string lastName)
        {
            PubContext _context = new PubContext();

            var author = _context.Authors.FirstOrDefault(a => a.LastName == lastName);

            var author2 = _context.Authors.OrderByDescending(a => a.FirstName)
                .FirstOrDefault(a => a.LastName == lastName);

            return true;
        }

        public bool QueryFilters(string name, string filters)
        {
            PubContext _context = new PubContext();

            //var authors = _context.Authors.Where(s => s.FirstName == "Julie").ToList();

            ////for EF Cores Sql injection protection
            var authorsParameterised = _context.Authors.Where(s => s.FirstName == name).ToList();

            var authors = _context.Authors
                .Where(a => EF.Functions.Like(a.LastName, filters)).ToList();
            //starts with L % representing anything that comes therafter

            return true;
        }

        public bool RetrieveAndUpdateAuthor(string name, string newName)
        {
            PubContext _context = new PubContext();

            var author = _context.Authors.FirstOrDefault(a => a.FirstName == name);

            if (author == null)
            {
                return false;
            }

            author.FirstName = newName;

            _context.SaveChanges();

            return true;
        }

        public bool SkipAndTakeAuthors(int groupSize)
        {
            PubContext _context = new PubContext();

            for (int i = 0; i < 5; i++)
            {
                var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();

                Console.WriteLine($"Group {i}:");

                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.FirstName} {author.LastName}");

                }
            }

            return true;
        }

        public bool SortAuthors()
        {
            PubContext _context = new PubContext();

            var authorsByLastName = _context.Authors
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName).ToList();

            authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + " " + a.FirstName));

            var authorsDescending = _context.Authors
                .OrderByDescending(a => a.LastName)
                .ThenByDescending(a => a.FirstName).ToList();

            Console.WriteLine("**Descending Last and First**");

            authorsDescending.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));

            return true;
        }
    }
}
