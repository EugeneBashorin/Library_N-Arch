using DataAccessLayer.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class BookRepository : IBookRepository
    {
        string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Book> GetAll()
        {
            List<Book> booksList;
            try
            {
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
            }
            catch (SqlException ex)
            {
                throw new Exception("problem with sql: " + ex);
            }
            return booksList;
        }

        public Book GetItemById(int? id)
        {
            Book book = new Book();
            try
            {
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
            }
            catch (SqlException ex)
            {
                throw new Exception("problem with sql: " + ex);
            }
            return book;
        }

        public void Create(Book book)
        {
            string createBookExpression = $"INSERT INTO Books([Name], [Author], [Publisher],[Price]) VALUES('{book.Name}','{book.Author}','{book.Publisher}','{book.Price}')";
            try
            {
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
            catch (SqlException ex)
            {
                throw new Exception("problem with sql: " + ex);
            }
        }

        public void Update(int? Id, Book book)
        {
            try
            {
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
            catch (SqlException ex)
            {
                throw new Exception("problem with sql: " + ex);
            }
        }

        public void Delete(int? id)
        {
            try
            {
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
            catch (SqlException ex)
            {
                throw new Exception("problem with sql: " + ex);
            }
        }

        public List<Book> FilterByPublisher(string publisherName)
        {
            List<Book> booksList;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    booksList = new List<Book>();
                    if (connection != null)
                    {
                        string selectAllExpression = $"SELECT * FROM Books WHERE Publisher = '{publisherName}'";
                        SqlCommand command = new SqlCommand(selectAllExpression, connection);
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
            }
            catch (SqlException ex)
            {
                throw new Exception("problem with sql: " + ex);
            }
            return booksList;
        }

        public List<string> GetAllPublishers()
        {
            List<string> publishersList;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    publishersList = new List<string>();
                    if (connection != null)
                    {
                        string selectAllExpression = $"SELECT DISTINCT Publisher FROM Books";
                        SqlCommand command = new SqlCommand(selectAllExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                publishersList.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("problem with sql: " + ex);
            }
            return publishersList;
        }
    }
}