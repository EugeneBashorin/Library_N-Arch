using DataAccessLayer.Repositories;
using Entities.Configurations;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace BusinessLogicLayer
{
    public class BusinessLogicLayer
    {
        public BusinessLogicLayer()
        { }

        public static void Create(Book book)
        {
           BookRepository.Create(book);
        }

        public static List<Book> GetAll()
        {
            return BookRepository.GetAll();
        }

        public static Book GetItemById(int id)
        {
            return BookRepository.GetItemById(id);
        }

        public static void Update(int id, Book book)
        {
            BookRepository.Update(id, book);
        }

        public static void Delete(int id)
        {
            BookRepository.Delete(id);
        }

        public static void GetTxtList(/*this List<Book> list*/)
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

            using (StreamWriter sw = new StreamWriter(DomianConfiguration.booksWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public static void GetXmlList(/*this List<Book> xmlList*/)
        {
            List<Book> xmlList = BookRepository.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            using (FileStream fs = new FileStream(DomianConfiguration.booksWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

    }
}