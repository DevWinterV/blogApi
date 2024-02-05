using Microsoft.AspNetCore.Identity;

namespace WEB_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? GmailToken { get; set; } 
    }
}
