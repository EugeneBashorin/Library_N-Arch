using Entities.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.ViewModels
{
    public class NewspaperFilterModel
    {
        public List<Newspaper> Newspapers { get; set; }
        public SelectList NewspapersPublisher { get; set; }
    }
}