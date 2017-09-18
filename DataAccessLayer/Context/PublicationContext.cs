using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Context
{
    public class PublicationContext //: IDisposable
    {
        public Book Books { get; set; }
        public Magazine Magazines { get; set; }
        public Newspaper Newspapers { get; set; }

        public string ConnectionString { get; set; }

        //public PublicationContext()
        //{
        //    SqlConnection connection = new SqlConnection();
        //    SqlCommand command = new SqlCommand();
        //    command.Connection.Open();
        //}
        //Start connection
        public PublicationContext(string connectionString)
        {
            ConnectionString = connectionString;
            //SqlConnection connection = new SqlConnection(connectionString);
            //SqlCommand command = new SqlCommand();
            //command.Connection.Open();
        }

        //private bool disposedValue = false; // To detect redundant calls

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects).
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //        // TODO: set large fields to null.

        //        disposedValue = true;
        //    }
        //}
        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //// ~PublicationContext() {
        ////   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        ////   Dispose(false);
        //// }

        //// This code added to correctly implement the disposable pattern.
        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(true);
        //    // TODO: uncomment the following line if the finalizer is overridden above.
        //    GC.SuppressFinalize(this);
        //}
    }
}