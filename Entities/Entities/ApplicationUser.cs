using Microsoft.AspNet.Identity.EntityFramework;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}