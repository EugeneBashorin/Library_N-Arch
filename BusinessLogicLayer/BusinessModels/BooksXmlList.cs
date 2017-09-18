using DataAccessLayer.Repositories;
using Entities.Configurations;
using Entities.Entities;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BusinessLogicLayer.BusinessModels
{
    public class BooksXmlList
    {
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