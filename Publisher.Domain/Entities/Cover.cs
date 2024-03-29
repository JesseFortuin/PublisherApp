﻿namespace Publisher.Domain.Entities
{
    public class Cover
    {
        public int CoverId { get; set; }
        public string DesignIdeas { get; set; }
        public bool DigitalOnly { get; set; }
        public List<Artist> Artists { get; set; } = new List<Artist>();
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
