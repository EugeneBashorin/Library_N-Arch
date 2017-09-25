using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IHomeService
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

        Magazine GetMagazine(int? id);
        List<Magazine> GetMagazines();
        void AddMagazine(Magazine magazine);
        void UpdateMagazine(int? id, Magazine magazine);
        void DeleteMagazine(int? id);
        void GetMagazinesTxtList();
        void GetMagazinesXmlList();
        List<Magazine> CheckMagazinePublisher(string publisherName);
        List<string> GetMagazinesPublishers();

        Newspaper GetNewspaper(int? id);
        List<Newspaper> GetNewspapers();
        void AddNewspaper(Newspaper newspaper);
        void UpdateNewspaper(int? id, Newspaper newspaper);
        void DeleteNewspaper(int? id);
        void GetNewspapersTxtList();
        void GetNewspapersXmlList();
        List<Newspaper> CheckNewspaperPublisher(string publisherName);
        List<string> GetNewspapersPublishers();
    }
}
