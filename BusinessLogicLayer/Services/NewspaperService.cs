using AutoMapper;
using BusinessLogicLayer.DataTransferObject;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogicLayer.Services
{
    public class NewspaperService : INewspaperService
    {
        IUnitOfWork Database { get; set; }

        public NewspaperService(IUnitOfWork database)
        {
            Database = database;
        }

        public List<NewspaperDTO> GetNewspapers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Newspaper, NewspaperDTO>());
            return Mapper.Map<List<Newspaper>, List<NewspaperDTO>>(Database.Newspapers.GetAll());
        }

        public void AddItem(NewspaperDTO newspaperDto)
        {
            Newspaper newspaper = new Newspaper();
            Mapper.Initialize(cfg => cfg.CreateMap<NewspaperDTO, Newspaper>());
            newspaper.Id = newspaperDto.Id;
            newspaper.Name = newspaperDto.Name;
            newspaper.Category = newspaperDto.Category;
            newspaper.Publisher = newspaperDto.Publisher;
            newspaper.Price = newspaperDto.Price;
            Database.Newspapers.Create(newspaper);
        }

        public NewspaperDTO GetNewspaper(int? id)
        {
            if (id == null)
                throw new ValidationException("Newspaper Id not found", "");
            var newspaper = Database.Newspapers.GetItemById(id.Value);
            if (newspaper == null)
                throw new ValidationException("The Newspaper not found", "");

            Mapper.Initialize(cfg => cfg.CreateMap<Newspaper, NewspaperDTO>());
            return Mapper.Map<Newspaper, NewspaperDTO>(newspaper);
        }

        
        public void Update(int id, NewspaperDTO newspaperDto)
        {
            Newspaper newspaper = new Newspaper();
            Mapper.Initialize(cfg => cfg.CreateMap<NewspaperDTO, Newspaper>());
            newspaper.Id = newspaperDto.Id;
            newspaper.Name = newspaperDto.Name;
            newspaper.Category = newspaperDto.Category;
            newspaper.Publisher = newspaperDto.Publisher;
            newspaper.Price = newspaperDto.Price;
            Database.Newspapers.Update(id, newspaper);
        }

        public void Delete(int id)
        {
            Database.Newspapers.Delete(id);
        }


    }
}