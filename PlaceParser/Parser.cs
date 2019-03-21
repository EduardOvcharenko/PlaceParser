using Newtonsoft.Json;
using PlaceParser.Data;
using PlaceParser.Models;
using System.Net;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace PlaceParser 
{
    public class Parser : IParser
    {
        private readonly IDataBase _dataBase;
        
        public Parser(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }
        public void GetPlace()
        {
            decimal latitude = 0;
            decimal longitude = 0;
            for (latitude = 50.429111m; latitude < 50.445153m; latitude += 0.000500m)
            {
                for (longitude = 30.491111m; longitude < 30.565350m; longitude += 0.000500m)
                {
                    GoogleRequest(GetLocationString(latitude, longitude));
                }
            }
        }

        string GetLocationString(decimal lat, decimal lng)
        {
            string locationString = lat.ToString(CultureInfo.CreateSpecificCulture("de-DE")) + "," 
                + lng.ToString(CultureInfo.CreateSpecificCulture("de-DE"));
            return locationString;
        }
        void GoogleRequest(string locationString)
        {
            string url = @"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+locationString+"&radius=500&type=bar&key=AIzaSyDNRQleDItpHaASUNyg4nsgGMqtwt8IOLU";

            var result = new WebClient().DownloadString(url);
            GooglePlaceResponse placeResponse = JsonConvert.DeserializeObject<GooglePlaceResponse>(result);

            if (placeResponse.Status == "OK")
            {
                _dataBase.SavePlace(placeResponse);
            }   
        }

    }
}
