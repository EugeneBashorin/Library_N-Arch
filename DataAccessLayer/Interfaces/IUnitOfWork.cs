using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        //IRepository<Magazine> Magazines { get; }
        //IRepository<Newspaper> Newspapers { get; }
        void Save();
    }
}
