using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IArtistFacade
    {
        public bool AddExistingArtistToCover(int artistId, int coverId);

        public ArtistDto FindArtistById(int artistId);

        public bool CreateNewArtistWithNewCover(AddCoverAndArtistDto coverAndArtistDto);
    }
}