using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IMagazineRepository
    {
        List<Magazine> GetAll();
        Magazine GetItemById(int? id);
        void Create(Magazine item);
        void Update(int? id, Magazine item);
        void Delete(int? id);
        List<Magazine> FilterByPublisher(string publisherName);
        List<string> GetAllPublishers();
    }
}
