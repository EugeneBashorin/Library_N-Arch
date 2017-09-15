﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities.Configurations
{
    public static class DomianConfiguration
    {
        public static string booksWriteTxtPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/books.txt";
        public static string booksWriteXmlPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/books.xml";

        public static string magazinesWriteTxtPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/magazines.txt";
        public static string magazinesWriteXmlPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/magazines.xml";

        public static string newspapersWriteTxtPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/newspapers.txt";
        public static string newspapersWriteXmlPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/newspapers.xml";
    }
}