using BusinessLogicLayer.DataTransferObject;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookService
    {
        BookDTO GetBook(int? id);
        List<BookDTO> GetBooks();
        void AddItem(BookDTO bookBto);
        void Update(int id, BookDTO bookDTO);
        void Delete(int id);        
    }
}