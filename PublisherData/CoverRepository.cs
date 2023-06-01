using Microsoft.EntityFrameworkCore;
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

        public bool AddCover(Cover cover)
        {
            pubContext.Covers.Add(cover);

            pubContext.SaveChanges();

            return true;
        }

        public bool RemoveArtistFromCover(int coverId, int artistId)
        {
            var coverWithArtists = pubContext.Covers
                .Include(c => c.Artists.Where(a => a.ArtistId == artistId))
                .FirstOrDefault(c => c.CoverId == coverId);

            coverWithArtists.Artists.RemoveAt(0);

            pubContext.SaveChanges();

            return true;
        }

        public bool ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId)
        {
            var cover = pubContext.Covers.Include(c => c.Artists
            .Where(a => a.ArtistId == oldArtistId))
            .FirstOrDefault(c => c.CoverId == coverId);

            cover.Artists.RemoveAt(0);

            var updatedArtist = pubContext.Artists.Find(updatedArtistId);

            cover.Artists.Add(updatedArtist);

            pubContext.SaveChanges();

            return true;
        }

        public bool AddCoverToExistingBook(Cover cover, Book book)
        {
            book.Cover = cover;

            pubContext.SaveChanges();

            return true;
        }

        public Book GetBookWithCover(int bookId)
        {
            var book = pubContext.Books.Include(b => b.Cover)
                .FirstOrDefault(b => b.BookId == bookId);

            return book;
        }

        public Cover GetCoverWithArtist(int coverId, int artistId)
        {
            var cover = pubContext.Covers.Include(c => c.Artists
            .Where(a => a.ArtistId == artistId))
            .FirstOrDefault(c => c.CoverId == coverId);

            return cover;
        }
    }
}
