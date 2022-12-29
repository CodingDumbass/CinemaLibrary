using System;
using System.Collections.Generic;

namespace CinemaLibrary.Infrastructure.Models
{
    public partial class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
