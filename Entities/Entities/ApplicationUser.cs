using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
       public string IsBanned { get; set; }

       public virtual ClientProfile ClientProfile { get; set; }

    }
}