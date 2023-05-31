﻿using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface ICoverRepository
    {
        Cover FindCoverById(int id);

        public bool CreateCoverWithExistingAuthor(Artist artist, Cover cover);

        public bool RemoveArtistFromCover(int coverId, int artistId);

        public bool ReassignCoverArtist(int coverId, int oldArtistId, int updatedArtistId);
    }
}