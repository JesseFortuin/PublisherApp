using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Publisher.Application;
using Publisher.Shared.Dtos;

namespace Publisher.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IPublisherFacade publisherFacade;

        public AuthorController(IPublisherFacade publisherFacade)
        {
            this.publisherFacade = publisherFacade;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            var result = publisherFacade.GetAuthors();

            return Ok(result);
        }

        [HttpGet("withbooks")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthorsWithBooks()
        {
            var result = publisherFacade.GetAuthorsWithBooks();

            return Ok(result);
        }
        
        [HttpGet("booksby")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthorsByYearOfPublish(int publishedDuringAndAfter)
        {
            var result = publisherFacade.GetAuthorsByRecentBook(publishedDuringAndAfter);

            return Ok(result);
        }

        [HttpGet("authorId")]
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
            //return CreatedAtAction("GetAuthor", new {firstName = authorDto.FirstName}, authorDto);
        }

        //[HttpPost()]
        //public ActionResult AddAuthorWithBooks(AddAuthorDto authorDto, AddAuthorBookDto bookDto)
        //{
        //    publisherFacade.AddAuthorWithBook(authorDto, bookDto);

        //    return CreatedAtAction("GetAuthor", new { authorId = authorDto.FirstName }, authorDto);
        //}

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
