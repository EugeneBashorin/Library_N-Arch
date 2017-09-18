using DataAccessLayer.Connection;
using DataAccessLayer.Context;
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

        //public IRepository<Magazine> Magazines
        //{
        //    get
        //    {
        //        if (magazineRepository == null)
        //        {
        //            magazineRepository = new MagazineRepository();
        //        }
        //        return magazineRepository;
        //    }
        //}

        //public IRepository<Newspaper> Newspapers
        //{
        //    get
        //    {
        //        if (newspaperRepository == null)
        //        {
        //            newspaperRepository = new NewspaperRepository();
        //        }
        //        return newspaperRepository;
        //    }
        //}


        //private bool disposed = false;

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            connectDB.Dispose();
        //        }
        //        this.disposed = true;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}