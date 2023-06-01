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

            cover.Artists.Add(artist);

            var result = artistRepository.AddCover(cover);

            return result;
        }

        public bool CreateNewArtistWithNewCover(AddCoverAndArtistDto coverAndArtistDto)
        {
            var artist = new Artist 
            {
                FirstName = coverAndArtistDto.FirstName,
                LastName = coverAndArtistDto.LastName,
            };

            var cover = new Cover
            {
                DesignIdeas = coverAndArtistDto.DesignIdeas
            };

            artist.Covers.Add(cover);

            var result = artistRepository.AddArtist(artist);

            return result;
        }
    }
}
