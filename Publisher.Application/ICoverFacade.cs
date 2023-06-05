using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface ICoverFacade
    {
        public ApiResponseDto<CoverDto> FindCoverById(int coverId);

        public ApiResponseDto<bool> CreateNewCoverWithExistingArtist(int artistId, int bookId, AddCoverDto coverDto);

        public ApiResponseDto<bool> RemoveArtistFromCover(int coverId, int artistId);

        public ApiResponseDto<bool> ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId);

        public ApiResponseDto<bool> AddCoverToExistingBook(int bookId, AddCoverDto coverDto);
    }
}