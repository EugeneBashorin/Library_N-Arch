using Microsoft.AspNet.Identity.EntityFramework;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string IsBanned { get; set; }

       public virtual ClientProfile ClientProfile { get; set; }

    }
}