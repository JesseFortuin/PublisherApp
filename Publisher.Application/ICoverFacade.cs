using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface ICoverFacade
    {
        CoverDto FindCoverById(int coverId);

        public bool CreateNewCoverWithExistingArtist(int artistId, AddCoverDto coverDto);
    }
}