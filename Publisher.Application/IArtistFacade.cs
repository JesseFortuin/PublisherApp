using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IArtistFacade
    {
        public ApiResponseDto<bool> AddExistingArtistToCover(int artistId, int coverId);

        public ApiResponseDto<ArtistDto> FindArtistById(int artistId);

        public ApiResponseDto<bool> AddArtistWithNewCover(AddArtistWithNewCoverDto coverAndArtistDto);
    }
}