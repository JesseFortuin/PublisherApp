using Publisher.Domain.Entities;
using PublisherData;

namespace Publisher.Infrastructure
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly PubContext pubContext;

        public ArtistRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }

        public Artist FindArtistById(int artistId)
        {
            return pubContext.Artists.Find(artistId);
        }

        public bool ConnectExistingArtistAndCoverArtist(Cover cover, Artist artist)
        {
            //var artistA = pubContext.Artists.Find(1);

            //var artistB = pubContext.Artists.Find(2);

            //var coverA = pubContext.Covers.Find(1);

            //coverA.Artists.Add(artistA);

            //coverA.Artists.Add(artistB);
            cover.Artists.Add(artist);

            pubContext.SaveChanges();

            return true;
        }

        public bool CreateNewArtistWithNewCover(Cover cover, Artist artist)
        {
            var newArtist = artist;

            var newCover = cover;

            newArtist.Covers.Add(newCover);

            pubContext.Artists.Add(newArtist);

            pubContext.SaveChanges();

            return true;
        }
    }
}
