using System;

namespace Entity.Entities
{
    [Serializable]
    public class Book : PrintEdition
    {
        public string Author { get; set; }
     }
}