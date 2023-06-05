using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public class CoverFacade : ICoverFacade
    {
        private readonly ICoverRepository coverRepository;
        private readonly IArtistRepository artistRepository;
        private readonly IBookRepository bookRepository;

        public CoverFacade(
            ICoverRepository coverRepository,
            IArtistRepository artistRepository,
            IBookRepository bookRepository)
        {
            this.coverRepository = coverRepository;
            this.artistRepository = artistRepository;
            this.bookRepository = bookRepository;
        }

        public ApiResponseDto<CoverDto> FindCoverById(int coverId)
        {
            var cover = coverRepository.FindCoverById(coverId);

            if (cover == null)
            {
                return new ApiResponseDto<CoverDto>("Cover not found");
            }

            var coverDto = new CoverDto
            {
                CoverId = coverId,
                DesignIdeas = cover.DesignIdeas,
                OnlyDigital = cover.DigitalOnly
            };

            return new ApiResponseDto<CoverDto>(coverDto);
        }

        public ApiResponseDto<bool> CreateNewCoverWithExistingArtist(int artistId, int bookId ,AddCoverDto coverDto)
        {
            var artist = artistRepository.FindArtistById(artistId);

            if (artist == null)
            {
                return new ApiResponseDto<bool>("Artist not found");
            }

            var cover = new Cover 
            { 
                DesignIdeas = coverDto.DesignIdeas,
                BookId = bookId
            };

            cover.Artists.Add(artist);

            var result = coverRepository.AddCover(cover);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> RemoveArtistFromCover(int coverId, int artistId)
        {
            var cover = coverRepository.GetCoverWithArtist(coverId, artistId);

            if (cover == null)
            {
                return new ApiResponseDto<bool>("Cover with those values not found");
            }

            cover.Artists.RemoveAt(0);

            var result = coverRepository.AddCover(cover);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId)
        {
            var cover = coverRepository.GetCoverWithArtist(coverId, oldArtistId);

            if (cover == null)
            {
                return new ApiResponseDto<bool>("Cover with those values not found");
            }

            cover.Artists.RemoveAt(0);

            var artist = artistRepository.FindArtistById(updatedArtistId);

            if (artist == null)
            {
                return new ApiResponseDto<bool>("New artist not found");
            }

            cover.Artists.Add(artist);

            var result = artistRepository.AddCover(cover);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> AddCoverToExistingBook(int bookId, AddCoverDto coverDto)
        {
            var book = coverRepository.GetBookWithCover(bookId);

            if (book == null)
            {
                return new ApiResponseDto<bool>("Book not found");
            }

            var cover = new Cover 
            {
                DesignIdeas = coverDto.DesignIdeas
            };

            book.Cover = cover;

            var result = bookRepository.AddBook(book);

            return new ApiResponseDto<bool>(result);
        }
    }
}
