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

        public BookService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void /*MakeOrder*/CreateBook(BookDTO bookDto)
        {
            Book book = Database.Books.GetItemById(bookDto.Id);

            // валидация
            if (book == null)
                throw new ValidationException("The book was't been found", "");
            // применяем скидку
         //   decimal sum = new Discount(0.1m).GetDiscountedPrice(phone.Price);
         //   Order order = new Order
         //   {
         //       Date = DateTime.Now,
        //        Address = orderDto.Address,
        //        PhoneId = phone.Id,
        //        Sum = sum,
       //         PhoneNumber = orderDto.PhoneNumber
       //     };
            Database.Books.Create(book);
            Database.Save();
        }

        public IEnumerable<BookDTO/*PhoneDTO*/> /*GetPhones*/GetBooks()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO/*Phone, PhoneDTO*/>());
            return Mapper.Map<IEnumerable</*Phone*/Book>, List</*PhoneDTO*/BookDTO>>(Database.Books.GetAll()/*.Phones.GetAll()*/);
        }

        public /*PhoneDTO GetPhone*/BookDTO GetBook(int? id)
        {
            if (id == null)
                throw new ValidationException("Book Id not found", "");
            var book = Database.Books.GetItemById(id.Value);// Phones.Get(id.Value);
            if (book == null)
                throw new ValidationException("The Book not found", "");
            // применяем автомаппер для проекции Phone на PhoneDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());//<Phone, PhoneDTO>());
            return Mapper.Map<Book, BookDTO>(book);//<Phone, PhoneDTO>(phone);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}