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

        // VEEL OP VEEL RELATIE
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

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

            //config voor een op een relatie
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.user)
                .HasForeignKey<Profile>(p => p.UserId);

            // *** Veel-op-veel tussen Student en Course via Enrollment ***
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
