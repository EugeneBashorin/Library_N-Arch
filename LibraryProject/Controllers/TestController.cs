using Entities.Entities;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace LibraryProject.Controllers
{
    public class TestController : ApiController
    {
        public JsonResult<List<Book>> GetBook()
        {
            List<Book> list = new List<Book> {
                new Book{ Id = 16, Name = "djfgsjlflsgf", Author = "slktgh", Publisher = "bskjha;dkbglad", Price = 1566},
                new Book{Id = 16, Name = "djfgsjlflsgf", Author = "slktgh", Publisher = "bskjha;dkbglad", Price = 1566 }
            };
            //Book book = new Book();
            //book.Id = 16;
            //book.Name = "djfgsjlflsgf";
            //book.Author = "slktgh";
            //book.Publisher = "bskjha;dkbglad";
            //book.Price = 1566;

            return Json(list);
            //return Json(book);
        }
    }
}