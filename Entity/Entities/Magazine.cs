using System;

namespace Entity.Entities
    [Serializable]
    public class Magazine : PrintEdition
    {
        public string Category { get; set; }
    }
}