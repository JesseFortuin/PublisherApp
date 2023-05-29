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

        [HttpGet("coverId")]
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
    }
}
