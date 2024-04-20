using Microsoft.AspNetCore.Identity;

namespace PustokMVC.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
