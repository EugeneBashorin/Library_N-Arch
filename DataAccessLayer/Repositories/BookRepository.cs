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
    public class BookRepository// : IRepository<Book>
    {
        //private MobileContext db;

        //public PhoneRepository(MobileContext context)
        //{
        //    this.db = context;
        //}

        public static List<Book> GetAll()
        {
            string _connectionString = @"Data Source=DESKTOP-4IAPGK2;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf;Database=LibraryDB; Integrated Security=True";
            List<Book> booksList;
            using (SqlConnection connection = new SqlConnection(_connectionString))
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
            return booksList;
        }

        public static Book GetItemById(int id)
        {
            string _connectionString = @"Data Source=DESKTOP-4IAPGK2;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf;Database=LibraryDB; Integrated Security=True";

            Book book = new Book();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchBookByIdExpression = $"SELECT * FROM Books WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchBookByIdExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            book.Id = (int)reader.GetValue(0);
                            book.Name = (string)reader.GetValue(1);
                            book.Author = (string)reader.GetValue(2);
                            book.Publisher = (string)reader.GetValue(3);
                            book.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return book;
        }

        public static void Create(Book book)
        {
            string _connectionString = @"Data Source=DESKTOP-4IAPGK2;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf;Database=LibraryDB; Integrated Security=True";
            string createBookExpression = $"INSERT INTO Books([Name], [Author], [Publisher],[Price]) VALUES('{book.Name}','{book.Author}','{book.Publisher}','{book.Price}')";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(createBookExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }

        public static void Update(int Id, Book book) 
        {
            string _connectionString = @"Data Source=DESKTOP-4IAPGK2;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf;Database=LibraryDB; Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string editBookExpression = $"UPDATE Books SET Name = '{book.Name}', Author = '{book.Author}', Publisher = '{book.Publisher}', Price = '{book.Price}' WHERE Id = '{Id}'";
                    SqlCommand command = new SqlCommand(editBookExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public static void Delete(int id)
        {
            string _connectionString = @"Data Source=DESKTOP-4IAPGK2;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf;Database=LibraryDB; Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string deleteBookExpression = $"DELETE FROM Books WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(deleteBookExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public IEnumerable<Book> Find(Func<Book, Boolean> predicate)
        {
            return null; //db.Phones.Where(predicate).ToList();
        }
    }
}