using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public class ArtistFacade : IArtistFacade
    {
        private readonly IArtistRepository artistRepository;
        private readonly ICoverRepository coverRepository;

        public ArtistFacade(
            IArtistRepository artistRepository,
            ICoverRepository coverRepository)
        {
            this.artistRepository = artistRepository;
            this.coverRepository = coverRepository;
        }

        public ArtistDto FindArtistById(int artistId)
        {
            var artist = artistRepository.FindArtistById(artistId);

            var artistDto = new ArtistDto
            {
                FirstName = artist.FirstName,
                LastName = artist.LastName,
            };

            return artistDto;
        }

        public bool AddExistingArtistToCover(int artistId, int coverId)
        {
            var artist = artistRepository.FindArtistById(artistId);

            var cover = coverRepository.FindCoverById(coverId);

            var result = artistRepository.ConnectExistingArtistAndCoverArtist(cover, artist);

            return result;
        }

        public bool CreateNewArtistWithNewCover(AddCoverAndArtistDto coverAndArtistDto)
        {
            var Artist = new Artist 
            {
                FirstName = coverAndArtistDto.FirstName,
                LastName = coverAndArtistDto.LastName,
            };

            var Cover = new Cover
            {
                DesignIdeas = coverAndArtistDto.DesignIdeas
            };

            var result = artistRepository.CreateNewArtistWithNewCover(Cover, Artist);

            return result;
        }
    }
}
