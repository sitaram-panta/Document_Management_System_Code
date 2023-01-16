using DMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DMS.Web.Data
{
    public class DMSDbContext : DbContext
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
