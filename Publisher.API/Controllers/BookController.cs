using Microsoft.AspNetCore.Mvc;
using Publisher.Application;
using Publisher.Shared.Dtos;

namespace Publisher.API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookFacade bookFacade;

        public BookController(
            IBookFacade bookFacade)
        {
            this.bookFacade = bookFacade;
        }

        [HttpGet()]
        public ActionResult<ApiResponseDto<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = bookFacade.GetAllBooks();
            
            return Ok (books);
        }

        [HttpGet("cover")]
        public ActionResult<ApiResponseDto<IEnumerable<BookAndCoverDto>>> GetAllBooksWithCovers()
        {
            var books = bookFacade.GetAllBooksWithCovers();

            return Ok (books);
        }

        [HttpGet("{bookId}")]
        public ActionResult<ApiResponseDto<BookDto>> GetBookById(int bookId)
        {
            var book = bookFacade.GetBookById(bookId);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet("{title}")]
        public ActionResult<ApiResponseDto<BookDto>> GetBookByTile(string title)
        {
            var book = bookFacade.GetBookByTitle(title);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost()]
        public ActionResult<ApiResponseDto<bool>> AddManyBooks(AddBookDto[] books) 
        {
            var result = bookFacade.AddManyBooks(books);

            return Ok(result);
        }

        [HttpPost("cover")]
        public ActionResult<ApiResponseDto<bool>> AddBookWithCover(AddBookWithCoverDto bookAndCover)
        {
            var result = bookFacade.AddBookWithCover(bookAndCover);

            return Ok(result);
        }
    }
}
