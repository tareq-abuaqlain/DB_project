using System.ComponentModel.DataAnnotations;

namespace ProjectDB.Models
{
    public class Course
    {
        [Key]
        public int course_Id { get; set; }

        [Required]
        public string? title { get; set; }

        public Department? department { get; set; }

        [Required]
        public int credits { get; set; }
    }
}
