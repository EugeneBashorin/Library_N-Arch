using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
   public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetItemById(int id);
        List<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(int id,T item);
        void Delete(int id);
    }
}
