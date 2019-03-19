using Newtonsoft.Json;
using PlaceParser.Data;
using PlaceParser.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser
{
    public class Parser
    {
        DBFcker DB = new DBFcker();
        public void GetPlaces()
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
            string locationString = lat.ToString().Replace(".",",") + "," + lng.ToString().Replace(".", ",");
            return locationString;
        }
        void GoogleRequest(string locationString)
        {
            string url = @"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+locationString+"&radius=500&type=bar&key=AIzaSyDNRQleDItpHaASUNyg4nsgGMqtwt8IOLU";

            var result = new System.Net.WebClient().DownloadString(url);
            GooglePlaceResponse placeResponse = JsonConvert.DeserializeObject<GooglePlaceResponse>(result);

            if (placeResponse.Status == "OK")
            {
                DB.SaveGooglePlace(placeResponse);
            }
            
        }

    }
}
