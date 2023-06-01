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

        public CoverDto FindCoverById(int coverId)
        {
            var cover = coverRepository.FindCoverById(coverId);

            var coverDto = new CoverDto
            {
                CoverId = coverId,
                DesignIdeas = cover.DesignIdeas,
                OnlyDigital = cover.DigitalOnly
            };

            return coverDto;
        }

        public bool CreateNewCoverWithExistingArtist(int artistId, AddCoverDto coverDto)
        {
            var artist = artistRepository.FindArtistById(artistId);

            var cover = new Cover 
            { 
                DesignIdeas = coverDto.DesignIdeas
            };

            cover.Artists.Add(artist);

            var result = coverRepository.AddCover(cover);

            return result;
        }

        public bool RemoveArtistFromCover(int coverId, int artistId)
        {
            var cover = coverRepository.GetCoverWithArtist(coverId, artistId);

            cover.Artists.RemoveAt(0);

            var result = coverRepository.AddCover(cover);

            return result;
        }

        public bool ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId)
        {
            var cover = coverRepository.GetCoverWithArtist(coverId, oldArtistId);

            cover.Artists.RemoveAt(0);

            var artist = artistRepository.FindArtistById(updatedArtistId);

            cover.Artists.Add(artist);

            var result = artistRepository.AddCover(cover);

            return result;
        }

        public bool AddCoverToExistingBook(int bookId, AddCoverDto coverDto)
        {
            var book = coverRepository.GetBookWithCover(bookId);

            var cover = new Cover 
            {
                DesignIdeas = coverDto.DesignIdeas
            };

            book.Cover = cover;

            var result = bookRepository.AddBook(book);

            return result;
        }
    }
}
