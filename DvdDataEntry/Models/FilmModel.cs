using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdDataEntry.Models
{
    internal class FilmModel
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseYear { get; set; }
        public int RentalDuration { get; set; }
        public decimal RentalRate { get; set; }
        public int Length { get; set; }
        public decimal ReplacementCost { get; set; }
        public string Rating { get; set; }
        public string SpecialFeatures { get; set; }
        public string FullText { get; set; }
    }
}
