using System;
using System.Collections.Generic;

namespace CinemaLibrary.Infrastructure.Models
{
    public partial class Favourite
    {
        public Guid FavouriteId { get; set; }
        public Guid MovieId { get; set; }
    }
}
