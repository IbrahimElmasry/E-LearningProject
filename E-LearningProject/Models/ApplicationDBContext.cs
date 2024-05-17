using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;


namespace E_LearningProject.Models
{
    public class ApplicationDBContext : IdentityDbContext <IdentityUser> // Dbcontext main class dotNet 
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-L0U7MDC;Database=GraduateProjectDB;Integrated Security=True;Encrypt=False;");
        //}
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> op):base(op)
        {
            
        }
        

  



    }
} 