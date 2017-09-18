using AutoMapper;
using BusinessLogicLayer.DataTransferObject;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
            // применяем автомаппер для проекции Phone на PhoneDTO
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
    }
}