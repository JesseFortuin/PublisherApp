using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public class CoverFacade : ICoverFacade
    {
        private readonly ICoverRepository coverRepository;
        private readonly IArtistRepository artistRepository;

        public CoverFacade(
            ICoverRepository coverRepository,
            IArtistRepository artistRepository)
        {
            this.coverRepository = coverRepository;
            this.artistRepository = artistRepository;
        }

        public CoverDto FindCoverById(int coverId)
        {
            var cover = coverRepository.FindCoverById(coverId);

            var coverDto = new CoverDto
            {
                CoverId = coverId,
                DesignIdeas = cover.DesignIdeas,
                OnlyDigital = cover.DigitalOnly
            };

            return coverDto;
        }

        public bool CreateNewCoverWithExistingArtist(int artistId, AddCoverDto coverDto)
        {
            var artist = artistRepository.FindArtistById(artistId);

            var cover = new Cover 
            { 
                DesignIdeas = coverDto.DesignIdeas
            };

            var result = coverRepository.CreateCoverWithExistingAuthor(artist, cover);

            return result;
        }
    }
}
