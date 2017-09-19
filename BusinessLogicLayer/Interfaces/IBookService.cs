using BusinessLogicLayer.DataTransferObject;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookService
    {
        BookDTO GetBook(int? id);
        List<BookDTO> GetBooks();
        void AddItem(BookDTO bookBto);
        void Update(int id, BookDTO bookDTO);
        void Delete(int id);
        void GetTxtList();
        void GetXmlList();
        List<BookDTO> CheckBookPublisher(string publisherName);
        List<string> GetBooksPublishers();
    }
}