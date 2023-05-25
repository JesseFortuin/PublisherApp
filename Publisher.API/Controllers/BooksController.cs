using Microsoft.AspNetCore.Mvc;
using Publisher.Application;
using Publisher.Shared.Dtos;

namespace Publisher.API.Controllers
{
    [ApiController]
    [Route("api/authors/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookFacade bookFacade;

        public BooksController(
            IBookFacade bookFacade)
        {
            this.bookFacade = bookFacade;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<BookDto>> GetAllBooks()
        {
            var books = bookFacade.GetAllBooks();
            
            return Ok (books);
        }

        [HttpGet("{bookId}")]
        public ActionResult GetBookById(int bookId)
        {
            var book = bookFacade.GetBookById(bookId);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet("Title", Name = "GetBook")]
        public ActionResult GetBookByTile(string bookTitle)
        {
            var book = bookFacade.GetBookByTitle(bookTitle);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost()]
        public ActionResult AddManyBooks(AddBookDto[] books) 
        {
            bookFacade.AddManyBooks(books);

            return Ok();
        }
    }
}
