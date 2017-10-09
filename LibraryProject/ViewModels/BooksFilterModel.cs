using Entities.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.ViewModels
{
    public class BooksFilterModel
    {
        public List<Book> Books { get; set; }
        public SelectList BooksPublisher { get; set; }
    }
}