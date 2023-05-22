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

        public bool AddBooksToAuthor(params Book[] books)
        {
            pubContext.Books.AddRange(books);

            pubContext.SaveChanges();

            return true;
        }

        public bool GetAllBooks()
        {
            var books = pubContext.Books.Include(x => x.Author).ToList();

            foreach (var book in books)
            {
                Console.WriteLine($" Author: {book.Author.FirstName} {book.Author.LastName}, Book title: {book.Title}, Book id: {book.BookId}, published: {book.PublishDate}");
            }

            return true;
        }

        public Book GetBookById(int id)
        {
            return pubContext.Books.Include(x => x.Author).SingleOrDefault(x => x.BookId == id);
        }
    }
}
