using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface ICoverFacade
    {
        public CoverDto FindCoverById(int coverId);

        public bool CreateNewCoverWithExistingArtist(int artistId, AddCoverDto coverDto);

        public bool RemoveArtistFromCover(int coverId, int artistId);

        public bool ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId);

        public bool AddCoverToExistingBook(int bookId, AddCoverDto coverDto);
    }
}