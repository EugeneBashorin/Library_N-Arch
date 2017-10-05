using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using ConfigurationData.Configurations;
using DataAccessLayer.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BusinessLogicLayer.Services
{
    public class HomeService : IHomeService//     IBookService, IMagazineService, INewspaperService
    {
        IBookRepository BookDatabase { get; set; }
        IMagazineRepository MagazineDatabase { get; set; }
        INewspaperRepository NewspaperDatabase { get; set; }

        public HomeService(IBookRepository bookDatabase, IMagazineRepository magazineDatabase, INewspaperRepository newspaperDatabase)
        {
            BookDatabase = bookDatabase;
            MagazineDatabase = magazineDatabase;
            NewspaperDatabase = newspaperDatabase;
        }

        public List<Book> GetBooks()
        {
            return BookDatabase.GetAll();
            //return Database.Books.GetAll();
        }

        public void AddBook(Book book)
        {
            BookDatabase.Create(book);
            //Database.Books.Create(book);
        }

        public Book GetBook(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Book Id not found", "");
            }
            var book = BookDatabase.GetItemById(id.Value);
            //var book = Database.Books.GetItemById(id.Value);
            if (book == null)
                throw new ValidationException("The Book not found", "");

            return book;
        }

        public void UpdateBook(int? id, Book book)
        {
            BookDatabase.Update(id, book);
            //Database.Books.Update(id, book);
        }

        public void DeleteBook(int? id)
        {
            BookDatabase.Delete(id);
            //Database.Books.Delete(id);
        }

        public void GetBooksTxtList()
        {
            List<Book> list = BookDatabase.GetAll();
            //List<Book> list = Database.Books.GetAll();
            StringBuilder result = new StringBuilder(130);
            if (list.Count > 0)
            {
                foreach (Book item in list)
                {
                    result.AppendLine($"Name: {item.Name} Author: {item.Author} Publisher: {item.Publisher} Price: {item.Price.ToString()}");
                }
            }

            using (StreamWriter sw = new StreamWriter(WritePathConfiguration.booksWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public void GetBooksXmlList()
        {
            List<Book> xmlList = BookDatabase.GetAll();
            //List<Book> xmlList = Database.Books.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            using (FileStream fs = new FileStream(WritePathConfiguration.booksWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

        public List<Book> CheckBookPublisher(string publisherName)
        {
            List<Book> bookList;
            if (!String.IsNullOrEmpty(publisherName))
            {
                bookList = BookDatabase.FilterByPublisher(publisherName);
                //bookList = Database.Books.FilterByPublisher(publisherName);
            }
            else
            {
                bookList = BookDatabase.GetAll();
                //bookList = Database.Books.GetAll();
            }
            return bookList;
        }

        public List<string> GetBooksPublishers()
        {
            List<string> booksPublishers = BookDatabase.GetAllPublishers();
            //List<string> booksPublishers = Database.Books.GetAllPublishers();
            return booksPublishers;
        }

        public List<Magazine> GetMagazines()
        {
            return MagazineDatabase.GetAll();
            //return Database.Magazines.GetAll();
        }

        public void AddMagazine(Magazine magazine)
        {
            MagazineDatabase.Create(magazine);
            //Database.Magazines.Create(magazine);
        }

        public Magazine GetMagazine(int? id)
        {
            if (id == null)
                throw new ValidationException("Magazine Id not found", "");

            var magazine = MagazineDatabase.GetItemById(id.Value);
            //var magazine = Database.Magazines.GetItemById(id.Value);
            if (magazine == null)
                throw new ValidationException("The Magazine not found", "");
            return magazine;
        }

        public void UpdateMagazine(int? id, Magazine magazine)
        {
            MagazineDatabase.Update(id, magazine);
            //Database.Magazines.Update(id, magazine);
        }

        public void DeleteMagazine(int? id)
        {
            MagazineDatabase.Delete(id);
            //Database.Magazines.Delete(id);
        }

        public void GetMagazinesTxtList()
        {
            List<Magazine> list = MagazineDatabase.GetAll();
            //List<Magazine> list = Database.Magazines.GetAll();
            StringBuilder result = new StringBuilder(130);
            if (list.Count > 0)
            {
                foreach (Magazine item in list)
                {
                    result.AppendLine($"Name: {item.Name} Category: {item.Category} Publisher: {item.Publisher} Price: {item.Price.ToString()}");
                }
            }
            using (StreamWriter sw = new StreamWriter(WritePathConfiguration.magazinesWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public void GetMagazinesXmlList()
        {
            List<Magazine> xmlList = MagazineDatabase.GetAll();
            //List<Magazine> xmlList = Database.Magazines.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Magazine>));
            using (FileStream fs = new FileStream(WritePathConfiguration.magazinesWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

        public List<Magazine> CheckMagazinePublisher(string publisherName)
        {
            List<Magazine> magazineList;
            if (!String.IsNullOrEmpty(publisherName))
            {
                magazineList = MagazineDatabase.FilterByPublisher(publisherName);
                //magazineList = Database.Magazines.FilterByPublisher(publisherName);
            }
            else
            {
                magazineList = MagazineDatabase.GetAll();
                //magazineList = Database.Magazines.GetAll();
            }
            return magazineList;
        }

        public List<string> GetMagazinesPublishers()
        {
            List<string> magazinesPublishers = MagazineDatabase.GetAllPublishers();
            //List<string> magazinesPublishers = Database.Magazines.GetAllPublishers();
            return magazinesPublishers;
        }

        public List<Newspaper> GetNewspapers()
        {
            return NewspaperDatabase.GetAll();
            //return Database.Newspapers.GetAll();
        }

        public void AddNewspaper(Newspaper newspaper)
        {
            NewspaperDatabase.Create(newspaper);
            //Database.Newspapers.Create(newspaper);
        }

        public Newspaper GetNewspaper(int? id)
        {
            if (id == null)
                throw new ValidationException("Newspaper Id not found", "");
            var newspaper = NewspaperDatabase.GetItemById(id.Value); 
            //var newspaper = Database.Newspapers.GetItemById(id.Value);
            if (newspaper == null)
                throw new ValidationException("The Newspaper not found", "");
            return newspaper;
        }

        public void UpdateNewspaper(int? id, Newspaper newspaper)
        {
            NewspaperDatabase.Update(id, newspaper);
            //Database.Newspapers.Update(id, newspaper);
        }

        public void DeleteNewspaper(int? id)
        {
            NewspaperDatabase.Delete(id);
            //Database.Newspapers.Delete(id);
        }

        public void GetNewspapersTxtList()
        {
            List<Newspaper> list = NewspaperDatabase.GetAll();
            //List<Newspaper> list = Database.Newspapers.GetAll();
            StringBuilder result = new StringBuilder(130);
            if (list.Count > 0)
            {
                foreach (Newspaper item in list)
                {
                    result.AppendLine($"Name: {item.Name} Category: {item.Category} Publisher: {item.Publisher} Price: {item.Price.ToString()}");
                }
            }
            using (StreamWriter sw = new StreamWriter(WritePathConfiguration.newspapersWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public void GetNewspapersXmlList()
        {
            List<Newspaper> xmlList = NewspaperDatabase.GetAll();
            //List<Newspaper> xmlList = Database.Newspapers.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Newspaper>));
            using (FileStream fs = new FileStream(WritePathConfiguration.newspapersWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

        public List<Newspaper> CheckNewspaperPublisher(string publisherName)
        {
            List<Newspaper> newspaperList;
            if (!String.IsNullOrEmpty(publisherName))
            {
                newspaperList = NewspaperDatabase.FilterByPublisher(publisherName);
                //newspaperList = Database.Newspapers.FilterByPublisher(publisherName);
            }
            else
            {
                newspaperList = NewspaperDatabase.GetAll();
                //newspaperList = Database.Newspapers.GetAll();
            }
            return newspaperList;
        }

        public List<string> GetNewspapersPublishers()
        {         
            List<string> newspapersPublishers = NewspaperDatabase.GetAllPublishers();
            //List<string> newspapersPublishers = Database.Newspapers.GetAllPublishers();
            return newspapersPublishers;
        }

        public void UpdateBook(int id, Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }
    }
}