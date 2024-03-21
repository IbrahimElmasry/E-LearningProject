using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_LearningProject.Models
{
    public class Course
    {
        
        public int CourseId { get; set; }

        
        public string CourseName { get; set; }
        public string? Description { get; set; }
        public string? Instructor { get; set; }
        public string Link { get; set; }

        [ForeignKey("category")]
        public int Category_Id { get; set; }

        public Category category { get; set; }
        //description hours instructor link 
    }
}
