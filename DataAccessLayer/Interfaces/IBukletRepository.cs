using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBukletRepository
    {
        List<Buklet> GetAll();
        Buklet GetItemById(int? id);
        void Create(Buklet item);
        void Update(int? id, Buklet item);
        void Delete(int? id);
        List<Buklet> FilterByPublisher(string publisherName);
        List<string> GetAllPublishers();
    }
}
