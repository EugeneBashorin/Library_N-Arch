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
    public class HomeService : IHomeService
    {

        IBookRepository BookRepository { get; set; }
        IMagazineRepository MagazineRepository { get; set; }
        INewspaperRepository NewspaperRepository { get; set; }

        public HomeService(IBookRepository bookRepository, IMagazineRepository magazineRepository, INewspaperRepository newspaperRepository)
        {
            BookRepository = bookRepository;
            MagazineRepository = magazineRepository;
            NewspaperRepository = newspaperRepository;
        }

        public List<Book> GetBooks()
        {
            return BookRepository.GetAll();
        }

        public void AddBook(Book book)
        {
            BookRepository.Create(book);
        }

        public Book GetBook(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Book Id not found", "");
            }
            var book = BookRepository.GetItemById(id.Value);
            if (book == null)
            {
                throw new ValidationException("The Book not found", "");
            }
            return book;
        }

        public void UpdateBook(int? id, Book book)
        {
            BookRepository.Update(id, book);
        }

        public void DeleteBook(int? id)
        {
            BookRepository.Delete(id);
        }

        public void GetBooksTxtList()
        {
            List<Book> list = BookRepository.GetAll();
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
            List<Book> xmlList = BookRepository.GetAll();
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
                bookList = BookRepository.FilterByPublisher(publisherName);
            }
            else
            {
                bookList = BookRepository.GetAll();
            }
            return bookList;
        }

        public List<string> GetBooksPublishers()
        {
            List<string> booksPublishers = BookRepository.GetAllPublishers();
            return booksPublishers;
        }

        public List<Magazine> GetMagazines()
        {
            return MagazineRepository.GetAll();
        }

        public void AddMagazine(Magazine magazine)
        {
            MagazineRepository.Create(magazine);
        }

        public Magazine GetMagazine(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Magazine Id not found", "");
            }
            var magazine = MagazineRepository.GetItemById(id.Value);
            if (magazine == null)
            {
                throw new ValidationException("The Magazine not found", "");
            }
            return magazine;
        }

        public void UpdateMagazine(int? id, Magazine magazine)
        {
            MagazineRepository.Update(id, magazine);
        }

        public void DeleteMagazine(int? id)
        {
            MagazineRepository.Delete(id);
        }

        public void GetMagazinesTxtList()
        {
            List<Magazine> list = MagazineRepository.GetAll();
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
            List<Magazine> xmlList = MagazineRepository.GetAll();
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
                magazineList = MagazineRepository.FilterByPublisher(publisherName);
            }
            else
            {
                magazineList = MagazineRepository.GetAll();
            }
            return magazineList;
        }

        public List<string> GetMagazinesPublishers()
        {
            List<string> magazinesPublishers = MagazineRepository.GetAllPublishers();
            return magazinesPublishers;
        }

        public List<Newspaper> GetNewspapers()
        {
            return NewspaperRepository.GetAll();
        }

        public void AddNewspaper(Newspaper newspaper)
        {
            NewspaperRepository.Create(newspaper);
        }

        public Newspaper GetNewspaper(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Newspaper Id not found", "");
            }
            var newspaper = NewspaperRepository.GetItemById(id.Value);
            if (newspaper == null)
            {
                throw new ValidationException("The Newspaper not found", "");
            }
            return newspaper;
        }

        public void UpdateNewspaper(int? id, Newspaper newspaper)
        {
            NewspaperRepository.Update(id, newspaper);
       }

        public void DeleteNewspaper(int? id)
        {
            NewspaperRepository.Delete(id);
        }

        public void GetNewspapersTxtList()
        {
            List<Newspaper> list = NewspaperRepository.GetAll();
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
            List<Newspaper> xmlList = NewspaperRepository.GetAll();
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
                newspaperList = NewspaperRepository.FilterByPublisher(publisherName);
            }
            else
            {
                newspaperList = NewspaperRepository.GetAll();
            }
            return newspaperList;
        }

        public List<string> GetNewspapersPublishers()
        {         
            List<string> newspapersPublishers = NewspaperRepository.GetAllPublishers();
            return newspapersPublishers;
        }    
    }
}