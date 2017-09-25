using System;

namespace Entities.Entities
{
    [Serializable]
    public class Newspaper : PrintEdition
    {
        public string Category { get; set; }
    }
}