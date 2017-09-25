using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    [Serializable]
    public class Book : PrintEdition
    {
        public string Author { get; set; }
     }
}