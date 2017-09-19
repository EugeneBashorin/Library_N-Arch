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
    public class BookService : IBookService
    {
        IUnitOfWork Database { get; set; }

        public BookService(IUnitOfWork database)
        {
            Database = database;
        }
       
        public List<BookDTO> GetBooks()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());
            return Mapper.Map<List<Book>, List<BookDTO>>(Database.Books.GetAll());
        }

        public void AddItem(BookDTO bookDto)
        {
            Book book = new Book();
            Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, Book>());
            book.Id = bookDto.Id;
            book.Name = bookDto.Name;
            book.Author = bookDto.Author;
            book.Publisher = bookDto.Publisher;
            book.Price = bookDto.Price;
            Database.Books.Create(book);
        }

        public BookDTO GetBook(int? id)
        {
            if (id == null)
                throw new ValidationException("Book Id not found", "");
            var book = Database.Books.GetItemById(id.Value);
            if (book == null)
                throw new ValidationException("The Book not found", "");
            // применяем автомаппер для проекции Phone на PhoneDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());
            return Mapper.Map<Book, BookDTO>(book);
        }

        public void Update(int id, BookDTO bookDto)
        {
            Book book = new Book();
            Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, Book>());
            book.Id = bookDto.Id;
            book.Name = bookDto.Name;
            book.Author = bookDto.Author;
            book.Publisher = bookDto.Publisher;
            book.Price = bookDto.Price;
            Database.Books.Update(id, book);           
        }
        public void Delete(int id)
        {
            Database.Books.Delete(id);
        }
    }
}