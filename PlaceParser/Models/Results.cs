using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser.Models
{
    public class Results
    {
        public Geometry Geometry { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string PlaceId { get; set; }
        public string Rating { get; set; }
        public string Vicinity { get; set; }
    }
}
