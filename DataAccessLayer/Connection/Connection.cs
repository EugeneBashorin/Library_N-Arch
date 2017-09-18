using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Connection
{
    public static class Connection
    {
        private static void ExecuteSqlCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { }
            }
        }

        private static void GetSqlCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                    }
                }
            }
        }
    }

    
 }

//public class Coonection<T> where T : class //: DbContext
//{
//    public Book Books { get; set; }

//    private static void GetSqlCommand(string queryString, string connectionString, T item)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            SqlCommand command = new SqlCommand(queryString, connection);
//            command.Connection.Open();
//            SqlDataReader reader = command.ExecuteReader();
//            if (reader.HasRows)
//            {
//                while (reader.Read())
//                {
//                    ReadDataReader(item,reader);
//                }
//            }
//        }
//    }
//    public void ReadDataReader(T book, SqlDataReader reader)
//    {
//        if (book is Book)
//        {
//            book.Id = (int)reader.GetValue(0);
//            book.Name = (string)reader.GetValue(1);
//            book.Author = (string)reader.GetValue(2);
//            book.Publisher = (string)reader.GetValue(3);
//            book.Price = (int)reader.GetValue(4);
//        }
//    }
//    public void read(Magazine magazine, SqlDataReader reader)
//    {
//        magazine.Id = (int)reader.GetValue(0);
//        magazine.Name = (string)reader.GetValue(1);
//        magazine.Category = (string)reader.GetValue(2);
//        magazine.Publisher = (string)reader.GetValue(3);
//        magazine.Price = (int)reader.GetValue(4);
//    }
//    public void read(Newspaper newspaper, SqlDataReader reader)
//    {
//        newspaper.Id = (int)reader.GetValue(0);
//        newspaper.Name = (string)reader.GetValue(1);
//        newspaper.Category = (string)reader.GetValue(2);
//        newspaper.Publisher = (string)reader.GetValue(3);
//        newspaper.Price = (int)reader.GetValue(4);
//    }      
//}
//}
