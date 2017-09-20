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
    public class NewspaperService : INewspaperService
    {
        IUnitOfWork Database { get; set; }

        public NewspaperService(IUnitOfWork database)
        {
            Database = database;
        }

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
            using (StreamWriter sw = new StreamWriter(DomianConfiguration.newspapersWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public void GetNewspapersXmlList()
        {
            List<Newspaper> xmlList = Database.Newspapers.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Newspaper>));
            using (FileStream fs = new FileStream(DomianConfiguration.newspapersWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }

        public List<Newspaper> CheckNewspaperPublisher(string publisherName)
        {
            List<Newspaper> newspaperList;
            if (!String.IsNullOrEmpty(publisherName) && !publisherName.Equals("All"))
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
            newspapersPublishers.Add("All");
            return newspapersPublishers;
        }

    }
}