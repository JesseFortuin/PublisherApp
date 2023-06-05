using Microsoft.AspNetCore.Mvc;
using Publisher.Application;
using Publisher.Shared.Dtos;

namespace Publisher.API.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IPublisherFacade publisherFacade;

        public AuthorController(IPublisherFacade publisherFacade)
        {
            this.publisherFacade = publisherFacade;
        }

        [HttpGet()]
        public ActionResult<ApiResponseDto<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            var result = publisherFacade.GetAuthors();

            return Ok(result);
        }

        [HttpGet("sql")]
        public ActionResult<ApiResponseDto<IEnumerable<AuthorDto>>> GetAllAuthorsWithSql()
        {
            var result = publisherFacade.GetAuthorsWithSqlRaw();

            return Ok(result);
        }

        [HttpGet("books")]
        public ActionResult<ApiResponseDto<IEnumerable<AuthorWithBooksDto>>> GetAuthorsWithBooks()
        {
            var result = publisherFacade.GetAuthorsWithBooks();

            return Ok(result);
        }

        [HttpGet("books/{year}")]
        public ActionResult<ApiResponseDto<IEnumerable<AuthorDto>>> GetAuthorsByYearOfPublish(int year)
        {
            var result = publisherFacade.GetAuthorsByRecentBook(year);

            return Ok(result);
        }

        [HttpGet("{authorId}")]
        public ActionResult<ApiResponseDto<bool>> GetAuthorById(int authorId)
        {
            var result = publisherFacade.GetAuthorById(authorId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{firstName}")]
        public ActionResult<ApiResponseDto<bool>> GetAuthorByName(string firstName)
        {
            var result = publisherFacade.GetAuthorByName(firstName);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ApiResponseDto<bool>> AddAuthor(AddAuthorDto authorDto)
        {
            var result = publisherFacade.AddAuthor(authorDto);

            return Ok(result);
        }

        [HttpPost("books")]
        public ActionResult<ApiResponseDto<bool>> AddAuthorWithBooks(AddAuthorWithBookDto authorWithBookDto)
        {
            var result = publisherFacade.AddAuthorWithBook(authorWithBookDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponseDto<bool>> DeleteAuthor(int id)
        {
            var result = publisherFacade.DeleteAuthor(id);

            if (result == null)
            {
                return NotFound(result);
            }

            return NoContent();
        }
    }
}
