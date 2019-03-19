using Microsoft.EntityFrameworkCore;
using PlaceParser.Data;
using PlaceParser.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser
{
    public class DBFcker
    {
        private readonly ApplicationDbContext _context;
        public DBFcker()
        {
        }
        public DBFcker(ApplicationDbContext db)
        {
            _context = db;
        }
        public void SaveGooglePlace(GooglePlaceResponse placeResponse)
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

                    using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))// new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=PlaceParser;Trusted_Connection=True;"))
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
}
