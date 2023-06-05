namespace ProjectDB.Models
{
    public class CourseTeach
    {
        public Course? course { get; set; }
        public Instructor? instructor { get; set; }
        public string? topic { get; set; }
        public string? book { get; set; }
    }
}
