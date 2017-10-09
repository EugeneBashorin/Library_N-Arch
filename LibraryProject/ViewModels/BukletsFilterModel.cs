using Entities.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.ViewModels
{
    public class BukletsFilterModel
    {
        public List<Buklet> Buklets { get; set; }
        public SelectList BukletsPublisher { get; set; }
    }
}