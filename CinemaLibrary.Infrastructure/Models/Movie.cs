using System;
using System.Collections.Generic;

namespace CinemaLibrary.Infrastructure.Models
{
    public partial class Movie
    {
        public Guid MovieId { get; set; }
        public Guid CategoryId { get; set; }
        public int MovieLength { get; set; }
        public float MovieRating { get; set; }
        public string MovieName { get; set; } = null!;
    }
}
