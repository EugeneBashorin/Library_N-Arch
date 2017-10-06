using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Entities.Entities
{
    [Serializable]
    [DataContract]
    public class Buklet : PrintEdition
    {
        [DataMember]
        public string Author { get; set; }
    }
}