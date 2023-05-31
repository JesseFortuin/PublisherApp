using Microsoft.EntityFrameworkCore;
using Publisher.Domain.Entities;
using PublisherData;

namespace Publisher.Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly PubContext pubContext;

        public BookRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }

        public bool AddManyBooks(params Book[] books)
        {
            pubContext.Books.AddRange(books);

            pubContext.SaveChanges();

            return true;
        }

        public List<Book> GetAllBooks()
        {
            return pubContext.Books.Include(x => x.Author).ToList();
        }

        public List<Book> GetAllBooksWithCovers()
        {
            return pubContext.Books.Include(b => b.Cover).ToList();
        }

        public Book GetBookById(int id)
        {
            return pubContext.Books.Include(x => x.Author).SingleOrDefault(x => x.BookId == id);
        }

        public Book GetBookByTitle(string bookTitle)
        {
            return pubContext.Books.Include(x => x.Author).FirstOrDefault(x => x.Title == bookTitle);
        }

    }
}
