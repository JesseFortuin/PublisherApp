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

        public bool AddManyAuthors(params Author[] authors)
        {
            //pubContext.Authors.Add(author1);

            //pubContext.Authors.Add(author2);

            pubContext.SaveChanges();

            return true;
        }

        public bool FindAndPaginationQuery()
        {
            throw new NotImplementedException();
        }

        public bool GetAuthors()
        {
            var authors = pubContext.Authors.ToList();

            foreach (var author in authors)
            {
                Console.WriteLine(author.FirstName + " " + author.LastName);
            }

            return true;
        }

        public bool RetrieveAndUpdateMultipleAuthorsLastNames(string name, string updatedLastName)
        {
            var Authors = pubContext.Authors.Where(a => a.LastName == name).ToList();

            foreach (var author in Authors)
            {
                author.LastName = updatedLastName;
            }

            Console.WriteLine("Before" + pubContext.ChangeTracker.DebugView.ShortView);

            pubContext.ChangeTracker.DetectChanges();

            Console.WriteLine("After" + pubContext.ChangeTracker.DebugView.ShortView);

            pubContext.SaveChanges();

            return true;
        }

        public bool GetAuthorsWithBooks()
        {
            var authors = pubContext.Authors.Include(a => a.Books).ToList();

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
            var author = pubContext.Authors.FirstOrDefault(a => a.LastName == lastName);

            var author2 = pubContext.Authors.OrderByDescending(a => a.FirstName)
                .FirstOrDefault(a => a.LastName == lastName);

            return true;
        }

        public bool QueryFilters(string name, string filters)
        {
            //var authors = _context.Authors.Where(s => s.FirstName == "Julie").ToList();

            ////for EF Cores Sql injection protection
            var authorsParameterised = pubContext.Authors.Where(s => s.FirstName == name).ToList();

            var authors = pubContext.Authors
                .Where(a => EF.Functions.Like(a.LastName, filters)).ToList();
            //starts with L % representing anything that comes therafter

            return true;
        }

        public bool RetrieveAndUpdateAuthor(string name, string newName)
        {
            var author = pubContext.Authors.FirstOrDefault(a => a.FirstName == name);

            if (author == null)
            {
                return false;
            }

            author.FirstName = newName;

            pubContext.SaveChanges();

            return true;
        }

        public bool SkipAndTakeAuthors(int groupSize)
        {
            for (int i = 0; i < 5; i++)
            {
                var authors = pubContext.Authors.Skip(groupSize * i).Take(groupSize).ToList();

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
            var authorsByLastName = pubContext.Authors
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName).ToList();

            authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + " " + a.FirstName));

            var authorsDescending = pubContext.Authors
                .OrderByDescending(a => a.LastName)
                .ThenByDescending(a => a.FirstName).ToList();

            Console.WriteLine("**Descending Last and First**");

            authorsDescending.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));

            return true;
        }

        public Author FindAnAuthor(int authorId)
        {
            using var shortLivedContext = new PubContext();

            return shortLivedContext.Authors.Find(authorId);
        }

        public bool SaveAnAuthor(Author author)
        {
            using var newShortLivedContext = new PubContext();

            newShortLivedContext.Authors.Update(author);

            newShortLivedContext.SaveChanges();

            return true;
        }

        public bool DeleteAnAuthor(int authorId)
        {
            var author = pubContext.Authors.Find(authorId);

            if (author == null)
            {
                return false;
            }

            pubContext.Authors.Remove(author);

            pubContext.SaveChanges();

            return true;
        }

        public bool InsertMultipleAuthors()
        {
            pubContext.Authors.AddRange(new Author { FirstName = "Ruth", LastName = "Ozeki" },
                                        new Author { FirstName = "Sofia", LastName = "Segovia" },
                                        new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
                                        new Author { FirstName = "Hugh", LastName = "Howey" },
                                        new Author { FirstName = "Isabelle", LastName = "Allende" });

            pubContext.SaveChanges();

            return true;
        }
    }
}
