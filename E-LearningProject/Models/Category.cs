namespace E_LearningProject.Models
{
    public class Category
    {
        public int Id { get; set; } //primary key
        public string Category_Name { get; set; }
        
        public List<Course> Courses { get; set; }//navigation property
                                                 //each category contains list of courses
                                                 // ONE TO MANY REALTIONSHIP
    }
}
