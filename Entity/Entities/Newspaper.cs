using System;

namespace Entity.Entities
{
    [Serializable]
    public class Newspaper : PrintEdition
    {
        public string Category { get; set; }
    }
}