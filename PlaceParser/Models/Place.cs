using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser.Models
{
    public class Place
    {
        public Guid Id { get; set; }
        public string GoogleId { get; set; }
        public string PlaceId { get; set; }
        public Guid PlaceType { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Adress { get; set; }
        public string Location { get; set; }
    }
}
