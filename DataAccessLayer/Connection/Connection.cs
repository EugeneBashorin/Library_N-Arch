using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Connection
{
    public class PublicationContext : IDisposable
    {
        public Book Books { get; set; }
        public Magazine Magazines { get; set; }
        public Newspaper Newspapers { get; set; }

        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
            set { value = connectionString; }
        }

        public PublicationContext()
        { }
        //Start connection
        public PublicationContext(string connectionString)
        {
            ConnectionString = connectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection.Open();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PublicationContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }


    public class Connection
    {
        private Connection() { }
        private static Connection _ConsString = null;
        private String _String = null;

        public static string ConString
        {
            get
            {
                if (_ConsString == null)
                {
                    _ConsString = new Connection { _String = Connection.Connect() };
                    return _ConsString._String;
                }
                else
                    return _ConsString._String;
            }
        }
        public static string Connect()
        {
            //Build an SQL connection string
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = "SIPL35\\SQL2016".ToString(), // Server name
                InitialCatalog = "Join8ShopDB",  //Database
                UserID = "Sa",         //Username
                Password = "Sa123!@#",  //Password
            };
            return sqlString.ConnectionString;
        }

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
