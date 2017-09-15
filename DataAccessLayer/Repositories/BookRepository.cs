using DataAccessLayer.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Repositories
{
    public class BookRepository //: IRepository<Book>
    {
        public static string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\IdentityDb.mdf;Integrated Security=True;";
        //private MobileContext db;

        //public PhoneRepository(MobileContext context)
        //{
        //    this.db = context;
        //}

        public static List<Book> GetAll()
        {
            List<Book> booksList;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                booksList = new List<Book>();
                if (connection != null)
                {

                    Book book = new Book();
                    string newspaperSelectExpression = "SELECT * FROM Books";
                    SqlCommand command = new SqlCommand(newspaperSelectExpression, connection);
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
            return booksList;
        }

        public Book Get(int id)
        {
            return null;//db.Phones.Find(id);
        }

        public void Create(Book book)
        {
          //  db.Phones.Add(book);
        }

        public void Update(Book book)
        {
          //db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<Book> Find(Func<Book, Boolean> predicate)
        {
            return null; //db.Phones.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            //Phone book = db.Phones.Find(id);
            //if (book != null)
            //    db.Phones.Remove(book);
        }
    }
}