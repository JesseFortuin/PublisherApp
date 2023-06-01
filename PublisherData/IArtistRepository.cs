﻿using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface IArtistRepository
    {
        bool AddCover(Cover cover);

        Artist FindArtistById(int ArtistId);

        public bool AddArtist(Artist artist);

        public List<Artist> GetAuthorsWithCoversAndCollaborators();
    }
}