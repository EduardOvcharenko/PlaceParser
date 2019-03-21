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
        private readonly IParser _parser;
        public List<Place> Places { get; set; }
        
        public IndexModel(ApplicationDbContext db, IParser parser)
        {
            _context = db;
            _parser = parser;
        }
        public void OnGet()
        {
            Places = _context.Places.AsNoTracking().ToList();
        }
        public void OnPostLocations()
        {
            _parser.GetPlace();
        } 
    }
}
