using Publisher.Domain.Entities;
using PublisherData;

namespace Publisher.Infrastructure
{
    public class CoverRepository : ICoverRepository
    {
        private readonly PubContext pubContext;

        public CoverRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }

        public Cover FindCoverById(int id)
        {
            return pubContext.Covers.Find(id);
        }

        public bool CreateCoverWithExistingAuthor(Artist artist, Cover cover)
        {
            cover.Artists.Add(artist);

            pubContext.Covers.Add(cover);

            pubContext.SaveChanges();

            return true;
        }
    }
}
