using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    [Serializable]
    public class Magazine : PrintEdition
    {
        [UIHint("String")]
        public string Category { get; set; }
    }
}