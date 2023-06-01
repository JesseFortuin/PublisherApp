using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface ICoverRepository
    {
        public Cover FindCoverById(int id);

        public bool AddCover(Cover cover);

        public Book GetBookWithCover(int bookId);

        public Cover GetCoverWithArtist(int coverId, int artistId);

        public bool RemoveArtistFromCover(int coverId, int artistId);

        public bool ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId);

        public bool AddCoverToExistingBook(Cover cover, Book book);
    }
}