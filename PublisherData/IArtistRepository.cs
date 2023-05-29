using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface IArtistRepository
    {
        bool ConnectExistingArtistAndCoverArtist(Cover cover, Artist artist);

        Artist FindArtistById(int ArtistId);

        public bool CreateNewArtistWithNewCover(Cover cover, Artist artist);
    }
}