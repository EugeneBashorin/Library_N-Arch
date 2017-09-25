using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    [Serializable]
    public abstract class PrintEdition
    {   
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        //[Range(0,10000)]
        [Required(ErrorMessage = "Incorrect value")]
        public int Price { get; set; }

        [UIHint("String")]
        [Required(ErrorMessage = "Please enter a publisher")]
        public string Publisher { get; set; }

        public int? Id { get; set; }
    }
}