using BusinessLogicLayer.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface INewspaperService
    {
        NewspaperDTO GetNewspaper(int? id);
        List<NewspaperDTO> GetNewspapers();
        void AddItem(NewspaperDTO newspaperDto);
        void Update(int id, NewspaperDTO newspaperDto);
        void Delete(int id);
    }
}
