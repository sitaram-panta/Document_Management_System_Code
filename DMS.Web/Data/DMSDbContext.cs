using DMS.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DMS.Web.Data
{
    public class DMSDbContext : IdentityDbContext<IdentityUser>
    {

        public DMSDbContext(DbContextOptions<DMSDbContext> options)
        {

        }
        public DbSet<Document> Documents { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {

            OptionsBuilder.
                UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DMS;Integrated Security=true");

        }
    }
}
