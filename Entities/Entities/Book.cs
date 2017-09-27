using System;
using System.Runtime.Serialization;

namespace Entities.Entities
{
    [Serializable]
    [DataContract]
    public class Book : PrintEdition
    {
        [DataMember]
        public string Author { get; set; }
     }
}