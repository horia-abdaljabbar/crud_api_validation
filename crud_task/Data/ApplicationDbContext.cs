using crud_task.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_task.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       
       

        public DbSet<Product> Products { get; set; }

    }
}
