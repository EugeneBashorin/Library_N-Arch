using AutoMapper;
using BusinessLogicLayer.DataTransferObject;
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

        public List<MagazineDTO> GetMagazines()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Magazine, MagazineDTO>());
            return Mapper.Map<List<Magazine>, List<MagazineDTO>>(Database.Magazines.GetAll());
        }

        public void AddItem(MagazineDTO magazineDto)
        {
            Magazine magazine = new Magazine();
            Mapper.Initialize(cfg => cfg.CreateMap<MagazineDTO, Magazine>());
            magazine.Id = magazineDto.Id;
            magazine.Name = magazineDto.Name;
            magazine.Category = magazineDto.Category;
            magazine.Publisher = magazineDto.Publisher;
            magazine.Price = magazineDto.Price;
            Database.Magazines.Create(magazine);
        }

        public MagazineDTO GetMagazine(int? id)
        {
            if (id == null)
                throw new ValidationException("Magazine Id not found", "");
            var magazine = Database.Magazines.GetItemById(id.Value);
            if (magazine == null)
                throw new ValidationException("The Magazine not found", "");
 
            Mapper.Initialize(cfg => cfg.CreateMap<Magazine, MagazineDTO>());
            return Mapper.Map<Magazine, MagazineDTO>(magazine);
        }

        public void Update(int id, MagazineDTO magazineDto)
        {
            Magazine magazine = new Magazine();
            Mapper.Initialize(cfg => cfg.CreateMap<MagazineDTO, Magazine>());
            magazine.Id = magazineDto.Id;
            magazine.Name = magazineDto.Name;
            magazine.Category = magazineDto.Category;
            magazine.Publisher = magazineDto.Publisher;
            magazine.Price = magazineDto.Price;
            Database.Magazines.Update(id, magazine);
        }
        public void Delete(int id)
        {
            Database.Magazines.Delete(id);
        }

        public void GetTxtList()
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

        public void GetXmlList()
        {
            List<Magazine> xmlList = Database.Magazines.GetAll();
            XmlSerializer xs = new XmlSerializer(typeof(List<Magazine>));
            using (FileStream fs = new FileStream(DomianConfiguration.magazinesWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlList);
            }
        }
        public List<MagazineDTO> CheckMagazinePublisher(string publisherName)
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
            Mapper.Initialize(cfg => cfg.CreateMap<Magazine, MagazineDTO>());
            var magazineListDtos = Mapper.Map<List<Magazine>, List<MagazineDTO>>(magazineList);
            return magazineListDtos;
        }

        public List<string> GetMagazinesPublishers()
        {
            List<string> magazinesPublishers = Database.Magazines.GetAllPublishers();
            magazinesPublishers.Add("All");
            return magazinesPublishers;
        }

    }
}