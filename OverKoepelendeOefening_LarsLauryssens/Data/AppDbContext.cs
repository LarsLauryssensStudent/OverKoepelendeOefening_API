using Microsoft.EntityFrameworkCore;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.Data
{
    public class AppDbContext : DbContext 
    {

        //EEN OP VEEL RELATIES
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }


        // EEN OP EEN RELATIE
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options ) : base( options ) 
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //config voor een een op veel relatie.
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);


            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.user)
                .HasForeignKey<Profile>(p => p.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
