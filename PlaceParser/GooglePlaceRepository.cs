using PlaceParser.Data;
using PlaceParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser
{
    public class GooglePlaceRepository : IDataBase
    {
        private readonly ApplicationDbContext _context;
        public GooglePlaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SavePlace(GooglePlaceResponse placeResponse)
        {
            foreach (var result in placeResponse.Results)
            {
                if (_context.Places.All(u => u.GoogleId != result.Id)
                && result?.OpeningHours != null && result?.Geometry?.Location != null)
                {
                    var TypeId = _context.PlaceTypes
                                            .Where(t => t.Name == "Bar")
                                            .FirstOrDefault().Id;

                    Place place = new Place();

                    place.Id = Guid.NewGuid();
                    place.GoogleId = result.Id;
                    place.PlaceId = result.PlaceId;
                    place.PlaceType = TypeId;
                    place.Name = result.Name;
                    place.Rating = result.Rating;
                    place.Adress = result.Vicinity;
                    place.Location = new string($"{result.Geometry.Location.Latitude},{result.Geometry.Location.Longitude}");

                    _context.Places.Add(place);
                }
            }
        }
    }
}
