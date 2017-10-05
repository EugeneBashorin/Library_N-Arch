using Entities.Entities;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IHomeService : IBookService, IMagazineService, INewspaperService
    {
        
    }
}