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
    public class MagazineService : IMagazineService
    {
        IUnitOfWork Database { get; set; }

        public MagazineService(IUnitOfWork database)
        {
            Database = database;
        }

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
            using (StreamWriter sw = new StreamWriter(DomianConfiguration.magazinesWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public void GetMagazinesXmlList()
        {
            List<Magazine> xmlList = Database.Magazines.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Magazine>));
            using (FileStream fs = new FileStream(DomianConfiguration.magazinesWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }
        public List<Magazine> CheckMagazinePublisher(string publisherName)
        {
            List<Magazine> magazineList;
            if (!String.IsNullOrEmpty(publisherName) && !publisherName.Equals("All"))
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
            magazinesPublishers.Add("All");
            return magazinesPublishers;
        }

    }
}