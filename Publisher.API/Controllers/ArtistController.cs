using Microsoft.AspNetCore.Mvc;
using Publisher.Application;
using Publisher.Shared.Dtos;

namespace Publisher.API.Controllers
{
    [Route("api/artist")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistFacade artistFacade;

        public ArtistController(IArtistFacade artistFacade)
        {
            this.artistFacade = artistFacade;
        }

        [HttpGet("{artistId}")]
        public ActionResult<ApiResponseDto<ArtistDto>> FindArtistById(int artistId)
        {
            var result = artistFacade.FindArtistById(artistId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPatch]
        public ActionResult<ApiResponseDto<bool>> AddExistingArtistToCover(int ArtistId, int CoverId)
        {
            var result = artistFacade.AddExistingArtistToCover(ArtistId, CoverId);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ApiResponseDto<bool>> CreateNewArtistWithNewCover(AddArtistWithNewCoverDto coverAndArtistDto)
        {
            var result = artistFacade.AddArtistWithNewCover(coverAndArtistDto);

            return Ok(result);
        }
    }
}
