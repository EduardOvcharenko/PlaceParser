using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlaceParser.Data;
using PlaceParser.Models;

namespace PlaceParser.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Place> Places { get; set; }
        string location;
        decimal lat;
        decimal lng;
        public int requestTry = 0;
        public IndexModel(ApplicationDbContext db)
        {
            _context = db;
        }
        public void OnGet()
        {
            Places = _context.Places.AsNoTracking().ToList();
        }
        public void OnPostLocations()
        {
            for (lat = 50.429111m; lat < 50.445153m; lat += 0.000500m)
            {
                for (lng = 30.491111m; lng < 30.565350m; lng += 0.000500m)
                {
                    GoogleRequest(GetLocationString());
                }
            }
        }
        string GetLocationString()
        {
            return location = lat.ToString().Replace(',', '.') + "," + lng.ToString().Replace(',', '.');
        }
        ActionResult RefreshPage()
        {
            return RedirectToPage("Index");
        }
        void GoogleRequest(string location)
        {
            string url = @"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+location+"&radius=500&type=bar&key=AIzaSyDNRQleDItpHaASUNyg4nsgGMqtwt8IOLU";

            var result = new System.Net.WebClient().DownloadString(url);
            GooglePlaceResponse placeResponse = JsonConvert.DeserializeObject<GooglePlaceResponse>(result);
            if (placeResponse.Status == "OK")
            {
                SavePlace(placeResponse);
            }          
        }
        void SavePlace(GooglePlaceResponse placeResponse)
        {
            for (int i = 0; i < placeResponse.Results.Count(); i++)
            {
                if (_context.Places.All(u => u.GoogleId != placeResponse.Results[i].id) && placeResponse.Results[i].opening_hours != null)
                {
                    string rating = placeResponse.Results[i].rating;
                    if (placeResponse.Results[i].rating == null)
                    {
                        rating = " ";
                    }
                    Guid Type = new Guid("14d44fc8-d53e-4440-874f-d44d115b2bd6");
                    using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=PlaceParser;Trusted_Connection=True;"))
                    {
                        string query = "INSERT INTO dbo.Places (Id,GoogleId,PlaceId,PlaceType,Name,Rating,Adress,Location) " +
                            "VALUES (@id,@GoogleId,@PlaceId,@PlaceType,@Name,@Rating,@Adress,@Location)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                            command.Parameters.AddWithValue("@GoogleId", placeResponse.Results[i].id);
                            command.Parameters.AddWithValue("@PlaceId", placeResponse.Results[i].place_id);
                            command.Parameters.AddWithValue("@PlaceType", Type);
                            command.Parameters.AddWithValue("@Name", placeResponse.Results[i].name);
                            command.Parameters.AddWithValue("@Rating", rating);
                            command.Parameters.AddWithValue("@Adress", placeResponse.Results[i].vicinity);
                            command.Parameters.AddWithValue("@Location", placeResponse.Results[i].geometry.location.lat + "," + placeResponse.Results[i].geometry.location.lng);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
        }
    }

    public class GooglePlaceResponse
    {
        public string Status { get; set; }
        public Results[] Results { get; set; }
        
    }
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
    public class OpeningHours
    {
        public bool open_now { get; set; }
    }
    public class Geometry
    {
        public Location location { get; set; }
    }
    public class Location
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
