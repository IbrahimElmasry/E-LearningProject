using Microsoft.EntityFrameworkCore;


namespace E_LearningProject.Models
{
    public class ApplicationDBContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=GraduateProjectDB;Integrated Security=True;");
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }



    }
} 