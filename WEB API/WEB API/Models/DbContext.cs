using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WEB_API.Models
{
    public class ContactContext : IdentityDbContext<ApplicationUser>
        { 

        public ContactContext(DbContextOptions<ContactContext> options) : base(options) { 

        }
        public DbSet<Contacts> Contacts {  get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<News> Newss { get; set; }
        public DbSet<Comments> Comments { get; set; }


    }

}
