using Microsoft.EntityFrameworkCore;
using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;
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
            pubContext.Authors.AddRange(authors);

            pubContext.SaveChanges();

            return true;
        }

        public List<Author> GetAuthors()
        {
            return pubContext.Authors.ToList();
        }

        public List<Author> GetAuthorsByRecentBook(int publishedOnAndAfter)
        {
            return pubContext.Authors.Where(a => a.Books.Any(b => b.PublishDate.Year >= publishedOnAndAfter))
                .ToList();
        }

        public List<Author> RetrieveAuthorsByLastName(string lastName)
        {
            //separate methods retrieve authors where. then send back to save
            return pubContext.Authors.Where(a => a.LastName == lastName).ToList();
        }

        public List<Author> GetAuthorsWithBooks()
        {
            var authors = pubContext.Authors.Include(a => a.Books).ToList();

            return authors;
        }

        public bool UpdateAuthorBook(Author author, int bookId)
        {
            pubContext.Entry(author.Books[bookId]).State = EntityState.Modified;

            pubContext.SaveChanges();

            return true;
        }

        public Author MultiLevelInclude(int authorId)
        {
            var authorGraph = pubContext.Authors.AsNoTracking()
                .Include(a => a.Books)
                .ThenInclude(b => b.Cover)
                .ThenInclude(c => c.Artists)
                .FirstOrDefault(a => a.AuthorId == authorId);

            return authorGraph;
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

        public Author GetAuthorByName(string name)
        {
            var author = pubContext.Authors.FirstOrDefault(a => a.FirstName == name);

            return author;
        }

        public Author GetAuthorByLastName(string lastName)
        {
            var author = pubContext.Authors.FirstOrDefault(a => a.LastName == lastName);

            return author;
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

        //public Author FindAuthor(int authorId)
        //{
        //    return pubContext.Authors.Find(authorId);
        //}

        //public bool DeleteAuthor(Author author)
        //{
        //    if (author == null)
        //    {
        //        return false;
        //    }

        //    pubContext.Authors.Remove(author);

        //    pubContext.SaveChanges();

        //    return true;
        //}

        public Author FindAnAuthorById(int authorId)
        {
            return pubContext.Authors.Find(authorId);
        }

        public bool SaveAnAuthor(Author author)
        {
            pubContext.Authors.Update(author);

            pubContext.SaveChanges();

            return true;
        }

        public bool DeleteAnAuthor(int authorId)
        {
            //separate find
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

        public bool EagerLoadBooksWithAuthors()
        {
            //var authors = pubContext.Authors.Include(a => a.Books).ToList();

            var cutOffDate = new DateTime(2010,1,1);

            var authors = pubContext.Authors
                .Include(a => a.Books
                .Where(b => b.PublishDate >= cutOffDate)
                .OrderBy(b => b.Title)).ToList();
            
            authors.ForEach(a =>
            {
                Console.WriteLine($"{a.LastName} No.Books ({a.Books.Count})");

                a.Books.ForEach(b => Console.WriteLine("      "+ b.Title));
            });

            return true;
        }

        public bool Projections()
        {
            var anonymousTypes = pubContext.Authors
                .Select(a => new
                {
                    AuthorId = a.AuthorId,
                    Name = a.FirstName.First() + "" + a.LastName,
                    Books = a.Books //.Where(b => b.PublishDate.Year < 2000).Count()
                })
                .ToList();

            return true;
        }

        public bool ExplicitLoadCollection()
        {
            var author = pubContext.Authors.FirstOrDefault(a => a.LastName == "Howey");

            pubContext.Entry(author).Collection(a => a.Books).Load();

            return true;
        }

        public bool LazyLoadBooksFromAnAuthor()
        {
            var author = pubContext.Authors.FirstOrDefault(a => a.LastName == "Howey");

            foreach (var book in pubContext.Books)
            {
                Console.WriteLine(book.Title);
            }

            return true;
        }

        public Author GetAuthorByIdWithBooks(int authorId)
        {
            var author = pubContext.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == authorId);

            return author;
        }

        public bool CascadeDelete(int authorId)
        {
            var author = pubContext.Authors.Include(a => a.Books)
                .FirstOrDefault(a => a.AuthorId == authorId);

            pubContext.Remove(author);

            pubContext.SaveChanges();

            return true;
        }

        public List<Author> SimpleRawSql()
        {
            return pubContext.Authors.FromSqlRaw("Exec SelectAllAuthors").ToList();
        }

        public List<Author> RawSqlStoredProc(int yearStart, int yearEnd)
        {
            return pubContext.Authors
                .FromSqlRaw("AuthorsPublishedinYearRange {0}, {1}", yearStart, yearEnd)
                .ToList();
        }

        public List<Author> InterpolatedSqlStoredProc(int yearStart, int yearEnd)
        {
            return pubContext.Authors
                .FromSqlInterpolated($"AuthorsPublishedinYearRange {yearStart}, {yearEnd}")
                .ToList();
        }

        public List<AuthorByArtist> GetAuthorsWithArtists()
        {
            //These are not tracked
            //not all DbSet methods work with keyless entities
            //e.g. Find() fails when making sql
            var authorsAndArtists = pubContext.AuthorsByArtists.ToList();

            //var authorByArtist = pubContext.AuthorsByArtists.FirstOrDefault();

            //var filterAuthorAndArtist = pubContext.AuthorsByArtists
            //    .Where(a => a.Artist.StartsWith("K")).ToList();

            return authorsAndArtists;
        }

        //There is no safe way with concatentation!
        void ConcatenatedRawSql_Unsafe()
        {
            var lastnameStart = "L";

            var authors = pubContext.Authors
                .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '" + lastnameStart + "%'")
                .OrderBy(a => a.LastName).TagWith("Concatenated_Unsafe").ToList();
        }
        
        void FormattedRawSql_Unsafe()
        {
            var lastnameStart = "L";

            var sql = String.Format("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart);

            var authors = pubContext.Authors.FromSqlRaw(sql)
                .OrderBy(a => a.LastName).TagWith("Formatted_Unsafe").ToList();
        }

        void FormattedRawSql_Safe()
        {
            var lastnameStart = "L";

            var authors = pubContext.Authors
                .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
                .OrderBy(a => a.LastName).TagWith("Formatted_Safe").ToList();
        }

        void StringFromInterpolated_Unsafe()
        {
            var lastnameStart = "L";

            string sql = $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'";

            var authors = pubContext.Authors.FromSqlRaw(sql)
                .OrderBy(a => a.LastName).TagWith("Interpolated_Unsafe").ToList();
        }

        void StringFromInterpolated_StillUnsafe()
        {
            var lastnameStart = "L";

            var authors = pubContext.Authors
                .FromSqlRaw($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
                .OrderBy(a => a.LastName).TagWith("Interpolated_StillUnsafe").ToList();
        }

        void StringFromInterpolated_Safe()
        {
            var lastnameStart = "L";

            var authors = pubContext.Authors
                .FromSqlInterpolated($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
            .OrderBy(a => a.LastName).TagWith("Interpolated_Safe").ToList();
        }
    }
}
