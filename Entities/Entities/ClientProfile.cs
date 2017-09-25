using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [UIHint("String")]
        public string Name { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}