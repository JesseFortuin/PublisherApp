using Microsoft.AspNetCore.Mvc;
using Publisher.Application;
using Publisher.Shared.Dtos;

namespace Publisher.API.Controllers
{
    [ApiController]
    [Route("api/book")]
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

        [HttpGet("cover")]
        public ActionResult<IEnumerable<BookAndCoverDto>> GetAllBooksWithCovers()
        {
            var books = bookFacade.GetAllBooksWithCovers();

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

        [HttpGet("{title}", Name = "GetBook")]
        public ActionResult GetBookByTile(string title)
        {
            var book = bookFacade.GetBookByTitle(title);

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
