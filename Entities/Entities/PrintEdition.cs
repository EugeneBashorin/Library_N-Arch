using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Entities.Entities
{
    [Serializable]
    [DataContract]
    public abstract class PrintEdition
    {   
        [DataMember]
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Incorrect value")]
        public int Price { get; set; }

        [DataMember]
        [UIHint("String")]
        [Required(ErrorMessage = "Please enter a publisher")]
        public string Publisher { get; set; }

        [DataMember]
        public int? Id { get; set; }
    }
}