using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public class ArtistFacade : IArtistFacade
    {
        private readonly IArtistRepository artistRepository;
        private readonly ICoverRepository coverRepository;
        private readonly IBookRepository bookRepository;

        public ArtistFacade(
            IArtistRepository artistRepository,
            ICoverRepository coverRepository,
            IBookRepository bookRepository)
        {
            this.artistRepository = artistRepository;
            this.coverRepository = coverRepository;
            this.bookRepository = bookRepository;
        }

        public ApiResponseDto<ArtistDto> FindArtistById(int artistId)
        {
            var artist = artistRepository.FindArtistById(artistId);

            var artistDto = new ArtistDto
            {
                FirstName = artist.FirstName,
                LastName = artist.LastName,
            };

            return new ApiResponseDto<ArtistDto>(artistDto);
        }

        public ApiResponseDto<bool> AddExistingArtistToCover(int artistId, int coverId)
        {
            var artist = artistRepository.FindArtistById(artistId);

            var cover = coverRepository.FindCoverById(coverId);

            cover.Artists.Add(artist);

            var result = artistRepository.UpdateCover(cover);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> AddArtistWithNewCover(AddArtistWithNewCoverDto coverAndArtistDto)
        {
            var book = bookRepository.GetBookById(coverAndArtistDto.BookId);

            if (book == null)
            {
                return new ApiResponseDto<bool>("Book not found");
            }

            var artist = new Artist 
            {
                FirstName = coverAndArtistDto.FirstName,
                LastName = coverAndArtistDto.LastName,
            };

            var cover = new Cover
            {
                DesignIdeas = coverAndArtistDto.DesignIdeas,
                BookId = coverAndArtistDto.BookId
            };

            artist.Covers.Add(cover);

            var result = artistRepository.AddArtist(artist);

            return new ApiResponseDto<bool>(result);
        }
    }
}
