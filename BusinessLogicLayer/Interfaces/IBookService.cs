using BusinessLogicLayer.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookService
    {
        void CreateBook/*MakeOrder*/(BookDTO bookDto);
        /*PhoneDTO*/ BookDTO GetBook/*GetPhone*/(int? id);
        IEnumerable</*PhoneDTO*/BookDTO> /*GetPhones*/GetBooks();
        void Dispose();
    }
}