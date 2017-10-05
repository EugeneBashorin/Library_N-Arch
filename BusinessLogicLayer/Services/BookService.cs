using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Entities.Configurations;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BusinessLogicLayer.Services
{
    public class BookService : IBookService
    {
        //*********************
        
        //*********************
        IUnitOfWork Database { get; set; }

        public BookService(IUnitOfWork database)
        {
            Database = database;
        }

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

            using (StreamWriter sw = new StreamWriter(DomianConfiguration.booksWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public void GetBooksXmlList()
        {
            List<Book> xmlList = Database.Books.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            using (FileStream fs = new FileStream(DomianConfiguration.booksWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

            public List<Book> CheckBookPublisher(string publisherName)
        {
            List<Book> bookList;
            if (!String.IsNullOrEmpty(publisherName) && !publisherName.Equals("All"))
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
            booksPublishers.Add("All");
            return booksPublishers;
        }
    }
}