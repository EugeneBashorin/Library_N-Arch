using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetItemById(int? id);
        void Create(T item);
        void Update(int? id,T item);
        void Delete(int? id);
        List<T> FilterByPublisher(string publisherName);
        List<string> GetAllPublishers();
    }
}
