using Microsoft.AspNetCore.Mvc;
using Publisher.Application;
using Publisher.Shared.Dtos;

namespace Publisher.API.Controllers
{
    [Route("api/cover")]
    [ApiController]
    public class CoverController : ControllerBase
    {
        private readonly ICoverFacade coverFacade;

        public CoverController(ICoverFacade coverFacade)
        {
            this.coverFacade = coverFacade;
        }

        [HttpGet("{coverId}")]
        public ActionResult<ApiResponseDto<CoverDto>> GetCoverById(int coverId)
        {
            var result = coverFacade.FindCoverById(coverId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("artist")]
        public ActionResult<ApiResponseDto<bool>> CreateNewCoverWithExistingArtist(int artistId, int bookId, AddCoverDto coverDto)
        {
            var result = coverFacade.CreateNewCoverWithExistingArtist(artistId, bookId, coverDto);

            if (result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("book")]
        public ActionResult<ApiResponseDto<bool>> CreateCoverWithExistingBook(int bookId, AddCoverDto coverDto)
        {
            var result = coverFacade.AddCoverToExistingBook(bookId, coverDto);

            if (result == null)
            {
                NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<ApiResponseDto<bool>> RemoveArtistFromCover(int coverId, int artistId) 
        { 
            var result = coverFacade.RemoveArtistFromCover(coverId, artistId);

            if (result == null)
            {
                return NotFound(result);
            }

            return NoContent();
        }

        [HttpPatch]
        public ActionResult<ApiResponseDto<bool>> ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId)
        {
            var result = coverFacade.ReassignCoverArtist(coverId, oldArtistId, updatedArtistId);
        
            if (result == null)
            {
                return NotFound(result);
            }        

            return Ok(result);
        }
    }
}
