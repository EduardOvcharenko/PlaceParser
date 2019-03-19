using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser.Models
{
    public class Results
    {
        public Geometry geometry { get; set; }
        public OpeningHours opening_hours { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string place_id { get; set; }
        public string rating { get; set; }
        public string vicinity { get; set; }
    }
}
