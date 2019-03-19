using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser.Models
{
    public class GooglePlaceResponse
    {
        public string Status { get; set; }
        public Results[] Results { get; set; }
    }
}
