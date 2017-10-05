using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface INewspaperRepository
    {
        List<Newspaper> GetAll();
        Newspaper GetItemById(int? id);
        void Create(Newspaper item);
        void Update(int? id, Newspaper item);
        void Delete(int? id);
        List<Newspaper> FilterByPublisher(string publisherName);
        List<string> GetAllPublishers();
    }
}
