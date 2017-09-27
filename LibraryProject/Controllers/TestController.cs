using Entities.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Results;

namespace LibraryProject.Controllers
{
    public class TestController : ApiController
    {
        //IHomeService homeService;// = new HomeService();

        public JsonResult<List<Book>> GetBook()
        {
            List<Book> booksList;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                booksList = new List<Book>();
                if (connection != null)
                {
                    string booksSelectAllExpression = "SELECT * FROM Books";
                    SqlCommand command = new SqlCommand(booksSelectAllExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            booksList.Add(new Book { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Author = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                        }
                    }
                }

            }
            //return booksList;
            //List<Book> list = new List<Book> {
            //    new Book{ Id = 16, Name = "djfgsjlflsgf", Author = "slktgh", Publisher = "bskjha;dkbglad", Price = 1566},
            //    new Book{Id = 16, Name = "djfgsjlflsgf", Author = "slktgh", Publisher = "bskjha;dkbglad", Price = 1566 }
            //};          
            return Json(booksList);
        }
    }
}