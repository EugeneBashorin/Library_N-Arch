using DataAccessLayer.Identity;
using Entities.Entities;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Book> Books { get; }
        IRepository<Magazine> Magazines { get; }
        IRepository<Newspaper> Newspapers { get; }       
    }
}
