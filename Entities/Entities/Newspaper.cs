using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Serializable]
    public class Newspaper : PrintEdition
    {
        [UIHint("String")]
        public string Category { get; set; }
    }
}