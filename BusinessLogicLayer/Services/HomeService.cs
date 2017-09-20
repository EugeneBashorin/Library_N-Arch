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
    public class HomeService : IHomeService//, IBookService, IMagazineService, INewspaperService
    {
        IUnitOfWork Database { get; set; }

        public HomeService(IUnitOfWork database)
        {
            Database = database;
        }

        //*****************************************IBookService**************************************
        public List<Book> GetBooks()
        {
            return Database.Books.GetAll();
        }

        public void AddBook(Book book)
        {
            Database.Books.Create(book);
        }

        public Book GetBook(int? id)
        {
            if (id == null)
                throw new ValidationException("Book Id not found", "");
            var book = Database.Books.GetItemById(id.Value);
            if (book == null)
                throw new ValidationException("The Book not found", "");

            return book;
        }

        public void UpdateBook(int id, Book book)
        {
            Database.Books.Update(id, book);
        }
        public void DeleteBook(int id)
        {
            Database.Books.Delete(id);
        }

        public void GetBooksTxtList()
        {
            List<Book> list = Database.Books.GetAll();
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
            List<Book> xmlList = Database.Books.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            using (FileStream fs = new FileStream(WritePathConfiguration.booksWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

        public List<Book> CheckBookPublisher(string publisherName)
        {
            List<Book> bookList;
            if (!String.IsNullOrEmpty(publisherName) && !publisherName.Equals(FilterConfiguration._ALL_PUBLISHER))
            {
                bookList = Database.Books.FilterByPublisher(publisherName);
            }
            else
            {
                bookList = Database.Books.GetAll();
            }
            return bookList;
        }

        public List<string> GetBooksPublishers()
        {
            List<string> booksPublishers = Database.Books.GetAllPublishers();
            booksPublishers.Add(FilterConfiguration._ALL_PUBLISHER);
            return booksPublishers;
        }

        //*****************************************IMagazineService**************************************
        public List<Magazine> GetMagazines()
        {
            return Database.Magazines.GetAll();
        }

        public void AddMagazine(Magazine magazine)
        {
            Database.Magazines.Create(magazine);
        }

        public Magazine GetMagazine(int? id)
        {
            if (id == null)
                throw new ValidationException("Magazine Id not found", "");
            var magazine = Database.Magazines.GetItemById(id.Value);
            if (magazine == null)
                throw new ValidationException("The Magazine not found", "");
            return magazine;
        }

        public void UpdateMagazine(int id, Magazine magazine)
        {
            Database.Magazines.Update(id, magazine);
        }
        public void DeleteMagazine(int id)
        {
            Database.Magazines.Delete(id);
        }

        public void GetMagazinesTxtList()
        {
            List<Magazine> list = Database.Magazines.GetAll();
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
            List<Magazine> xmlList = Database.Magazines.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Magazine>));
            using (FileStream fs = new FileStream(WritePathConfiguration.magazinesWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }
        public List<Magazine> CheckMagazinePublisher(string publisherName)
        {
            List<Magazine> magazineList;
            if (!String.IsNullOrEmpty(publisherName) && !publisherName.Equals(FilterConfiguration._ALL_PUBLISHER))
            {
                magazineList = Database.Magazines.FilterByPublisher(publisherName);
            }
            else
            {
                magazineList = Database.Magazines.GetAll();
            }
            return magazineList;
        }

        public List<string> GetMagazinesPublishers()
        {
            List<string> magazinesPublishers = Database.Magazines.GetAllPublishers();
            magazinesPublishers.Add(FilterConfiguration._ALL_PUBLISHER);
            return magazinesPublishers;
        }
        //*****************************************INewspaperService**************************************
        public List<Newspaper> GetNewspapers()
        {
            return Database.Newspapers.GetAll();
        }

        public void AddNewspaper(Newspaper newspaper)
        {
            Database.Newspapers.Create(newspaper);
        }

        public Newspaper GetNewspaper(int? id)
        {
            if (id == null)
                throw new ValidationException("Newspaper Id not found", "");
            var newspaper = Database.Newspapers.GetItemById(id.Value);
            if (newspaper == null)
                throw new ValidationException("The Newspaper not found", "");
            return newspaper;
        }

        public void UpdateNewspaper(int id, Newspaper newspaper)
        {
            Database.Newspapers.Update(id, newspaper);
        }

        public void DeleteNewspaper(int id)
        {
            Database.Newspapers.Delete(id);
        }

        public void GetNewspapersTxtList()
        {
            List<Newspaper> list = Database.Newspapers.GetAll();
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
            List<Newspaper> xmlList = Database.Newspapers.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Newspaper>));
            using (FileStream fs = new FileStream(WritePathConfiguration.newspapersWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

        public List<Newspaper> CheckNewspaperPublisher(string publisherName)
        {
            List<Newspaper> newspaperList;
            if (!String.IsNullOrEmpty(publisherName) && !publisherName.Equals(FilterConfiguration._ALL_PUBLISHER))
            {
                newspaperList = Database.Newspapers.FilterByPublisher(publisherName);
            }
            else
            {
                newspaperList = Database.Newspapers.GetAll();
            }
            return newspaperList;
        }

        public List<string> GetNewspapersPublishers()
        {
            List<string> newspapersPublishers = Database.Newspapers.GetAllPublishers();
            newspapersPublishers.Add(FilterConfiguration._ALL_PUBLISHER);
            return newspapersPublishers;
        }
    }
}