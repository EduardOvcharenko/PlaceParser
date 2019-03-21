using PlaceParser.Data;
using PlaceParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser
{
    public interface IDataBase
    {
        void SavePlace(GooglePlaceResponse placeResponse);
    }
}
