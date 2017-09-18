using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BookViewModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
    }
}