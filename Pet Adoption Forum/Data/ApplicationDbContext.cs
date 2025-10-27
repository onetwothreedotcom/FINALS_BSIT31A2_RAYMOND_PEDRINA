using Microsoft.EntityFrameworkCore;
using Pet_Adoption_Forum.Models;

namespace Pet_Adoption_Forum.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
    }
}
