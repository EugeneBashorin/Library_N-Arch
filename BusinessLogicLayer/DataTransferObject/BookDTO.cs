﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogicLayer.DataTransferObject
{
    public class BookDTO
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Price { get; set; }        
        public int Id { get; set; }
    }
}