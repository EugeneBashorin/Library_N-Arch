using System;

namespace Entities.Entities
{
    [Serializable]
    public class Magazine : PrintEdition
    {
        public string Category { get; set; }
    }
}