using Microsoft.AspNetCore.Identity;

namespace Cines_Flagg
{
    public class ApplicationUser : IdentityUser
    {
        public bool esAdmin { get; set; }
    }
}
