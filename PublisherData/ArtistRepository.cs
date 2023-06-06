using Microsoft.EntityFrameworkCore;
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

        public bool AddCover(Cover cover)
        {
            pubContext.Covers.Add(cover);

            pubContext.SaveChanges();

            return true;
        }

        public bool UpdateCover(Cover cover)
        {
            pubContext.Update(cover);

            pubContext.SaveChanges();

            return true;
        }

        public bool AddArtist(Artist artist)
        {
            pubContext.Artists.Add(artist);

            pubContext.SaveChanges();

            return true;
        }

        public List<Artist> GetAuthorsWithCoversAndCollaborators()
        {
            return pubContext.Artists.Include(a => a.Covers).ToList();
        }
    }
}
