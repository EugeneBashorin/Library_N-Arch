using DataAccessLayer.Repositories;
using Entities.Configurations;
using Entities.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLogicLayer.BusinessModels
{
    public class BooksTxtList
    {
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
    }
}