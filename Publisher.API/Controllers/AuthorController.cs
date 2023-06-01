using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        public ActionResult<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            var result = publisherFacade.GetAuthors();

            return Ok(result);
        }
        
        [HttpGet("sql")]
        public ActionResult<IEnumerable<AuthorDto>> GetAllAuthorsWithSql()
        {
            var result = publisherFacade.GetAuthorsWithSqlRaw();

            return Ok(result);
        }

        [HttpGet("books")]
        public ActionResult<IEnumerable<AuthorWithBooksDto>> GetAuthorsWithBooks()
        {
            var result = publisherFacade.GetAuthorsWithBooks();

            return Ok(result);
        }
        
        [HttpGet("books/{year}")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthorsByYearOfPublish(int year)
        {
            var result = publisherFacade.GetAuthorsByRecentBook(year);

            return Ok(result);
        }

        [HttpGet("{authorId}")]
        public ActionResult GetAuthorById(int authorId)
        {
            var result = publisherFacade.GetAuthorById(authorId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{firstName}", Name = "GetAuthor")]
        public ActionResult GetAuthorByName(string firstName)
        {
            var result = publisherFacade.GetAuthorByName(firstName);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddAuthor(AddAuthorDto authorDto)
        {
            publisherFacade.AddAuthor(authorDto);

            return Ok();
        }

        [HttpPost("books")]
        public ActionResult AddAuthorWithBooks(AddAuthorWithBookDto authorWithBookDto)
        {
            publisherFacade.AddAuthorWithBook(authorWithBookDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id) 
        {
            var result = publisherFacade.DeleteAuthor(id);

            if (result == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
