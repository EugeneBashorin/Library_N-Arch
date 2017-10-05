using DataAccessLayer.Context;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book GetItemById(int? id);
        void Create(Book item);
        void Update(int? id, Book item);
        void Delete(int? id);
        List<Book> FilterByPublisher(string publisherName);
        List<string> GetAllPublishers();      
    }
}
