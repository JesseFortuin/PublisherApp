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
        public ActionResult GetCoverById(int coverId)
        {
            var result = coverFacade.FindCoverById(coverId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateNewCoverWithExistingArtist(int artistId, AddCoverDto coverDto)
        {
            var result = coverFacade.CreateNewCoverWithExistingArtist(artistId, coverDto);

            return Ok();
        }

        [HttpDelete]
        public ActionResult RemoveArtistFromCover(int coverId, int artistId) 
        { 
            var result = coverFacade.RemoveArtistFromCover(coverId, artistId);

            if (result == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch]
        public ActionResult ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId)
        {
            var result = coverFacade.ReassignCoverArtist(coverId, oldArtistId, updatedArtistId);
        
            if (result == false)
            {
                return NotFound();
            }        

            return Ok();
        }
    }
}
