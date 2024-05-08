using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;


namespace E_LearningProject.Models
{
    public class ApplicationDBContext : DbContext // Dbcontext main class dotNet 
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-L0U7MDC;Database=GraduateProjectDB;Integrated Security=True;Encrypt=False;");
            //Connection String connect app with database 
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }



    }
} 