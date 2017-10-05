using Entities.Entities;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookService
    {      
        Book GetBook(int? id);
        List<Book> GetBooks();
        void AddBook(Book book);
        void UpdateBook(int? id, Book book);
        void DeleteBook(int? id);
        void GetBooksTxtList();
        void GetBooksXmlList();
        List<Book> CheckBookPublisher(string publisherName);
        List<string> GetBooksPublishers();
    }
}