namespace Publisher.Shared.Dtos
{
    public class ArtistsAndCoversDto
    {
        public int PrimaryArtistId { get; set; }
        public string ArtistName { get; set; }
        public List<string> CollaboratorName { get; set; } = new List<string>();
        public string DesignIdeas { get; set; }
        public int BookId { get; set; }
    }
}
