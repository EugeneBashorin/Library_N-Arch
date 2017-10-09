using Entities.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.ViewModels
{
    public class MagazineFilterModel
    {
        public List<Magazine> Magazines { get; set; }
        public SelectList MagazinesPublisher { get; set; }
    }
}