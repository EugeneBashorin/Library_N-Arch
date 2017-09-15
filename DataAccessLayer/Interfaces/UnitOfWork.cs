using DataAccessLayer.Connection;
using DataAccessLayer.Repositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private PublicationContext connectDB;
        private BookRepository bookRepository;
        //private MagazineRepository magazineRepository;
        //private NewspaperRepository newspaperRepository;

        public UnitOfWork(string connectionString)
        {
            connectDB = new PublicationContext(connectionString);
        }

        public IRepository<Book> Books
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(connectDB);
                }
                return bookRepository;
            }
        }
         
        public void executeQuery(string queryString)//Save()
        {

        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    connectDB.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}