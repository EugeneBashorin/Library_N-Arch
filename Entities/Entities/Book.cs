using System;

namespace Entities.Entities
{
    [Serializable]
    public class Book : PrintEdition
    {
        public string Author { get; set; }
     }
}