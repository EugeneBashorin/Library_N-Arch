using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Entities.Configurations;
using Entities.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BusinessLogicLayer
{
    public class BusinessLogicLayer
    {
        //PublicationContext publicationContext;
        //BookRepository bookRepository = new BookRepository(publicationContext);
        public BusinessLogicLayer()
        { }

        public void Create(Book book)//++
        {
          //  bookRepository.Create(book);
        }

        public List<Book> GetAll()
        {
            return null;// bookRepository.GetAll();
        }

        public Book GetItemById(int id)//++
        {
            return null;// bookRepository.GetItemById(id);
        }

        public void Update(int id, Book book)
        {
            //bookRepository.Update(id, book);
        }

        public void Delete(int id)//++
        {
           // bookRepository.Delete(id);
        }

        //public static void GetTxtList(/*this List<Book> list*/)
        //{
        //    BookRepository bookRepository = new BookRepository();
        //    List<Book> list = bookRepository.GetAll();
        //    StringBuilder result = new StringBuilder(130);

        //    if (list.Count > 0)
        //    {
        //        foreach (Book item in list)
        //        {
        //            result.AppendLine($"Name: {item.Name} Author: {item.Author} Publisher: {item.Publisher} Price: {item.Price.ToString()}");
        //        }
        //    }

        //    using (StreamWriter sw = new StreamWriter(DomianConfiguration.booksWriteTxtPath, false, System.Text.Encoding.Default))
        //    {
        //        sw.WriteLine(result);
        //    }
        //}

        //public static void GetXmlList(/*this List<Book> xmlList*/)
        //{
        //    BookRepository bookRepository = new BookRepository();
        //    List<Book> xmlList = bookRepository.GetAll();
        //    XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
        //    using (FileStream fs = new FileStream(DomianConfiguration.booksWriteXmlPath, FileMode.Create))
        //    {
        //        xs.Serialize(fs, xmlList);
        //    }
        //}

    }
}