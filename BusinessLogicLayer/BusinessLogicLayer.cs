using DataAccessLayer.Repositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogicLayer
{
    public class BusinessLogicLayer
    {
        public static List<Book> GetAll()
        {
            return BookRepository.GetAll();
        }
    }
}