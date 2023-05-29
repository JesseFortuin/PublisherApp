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
            var authors = pubContext.Authors.ToList();

            return authors;
        }

        public List<Author> GetAuthorsByRecentBook(int publishedOnAndAfter)
        {
            return pubContext.Authors.Where(a => a.Books.Any(b => b.PublishDate.Year >= publishedOnAndAfter))
                .ToList();
        }

        public bool RetrieveAndUpdateMultipleAuthorsLastNames(string name, string updatedLastName)
        {
            //separate methods retrieve authors where. then send back to save
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

        public bool AddNewBookToExistingAuthor(string authorsLastName, AddAuthorBookDto bookDto)
        {
            //find
            var author = pubContext.Authors.FirstOrDefault(a => a.LastName == authorsLastName);

            if (author == null)
            {
                return false;
            }

            author.Books.Add(
                new Book { Title = bookDto.Title, PublishDate = new DateTime(2012, 1, 1)}
                );

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
    }
}
