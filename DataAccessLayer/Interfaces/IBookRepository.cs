using Entities.Entities;
using System.Collections.Generic;

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
