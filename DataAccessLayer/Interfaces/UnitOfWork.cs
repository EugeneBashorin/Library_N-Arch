using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Entities.Entities;

namespace DataAccessLayer.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        //private PublicationContext connectDB;

        //private BookRepository bookRepository;
        //private MagazineRepository magazineRepository;
        //private NewspaperRepository newspaperRepository;

        //public UnitOfWork(string connectionString)
        //{
        //    connectDB = new PublicationContext(connectionString);
        //}

        //public IRepository<Book> Books
        //{
        //    get
        //    {
        //        if (bookRepository == null)
        //        {
        //            bookRepository = new BookRepository(connectDB);
        //        }
        //        return bookRepository;
        //    }
        //}

        //public IRepository<Magazine> Magazines
        //{
        //    get
        //    {
        //        if (magazineRepository == null)
        //        {
        //            magazineRepository = new MagazineRepository(connectDB);
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
        //            newspaperRepository = new NewspaperRepository(connectDB);
        //        }
        //        return newspaperRepository;
        //    }
        //}
    }
}