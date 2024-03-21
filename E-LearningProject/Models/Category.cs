namespace E_LearningProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Category_Name { get; set; }
        
        public List<Course> Courses { get; set; }//navigation prop==>each category contains list of courses
    }
}
