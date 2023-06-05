using Microsoft.Build.Framework;

namespace ProjectDB.Models
{
    public class Student
    {
        [Required]
        public int std_id { get; set; }

        [Required]

        public string? name { get; set; }
        public Department? department { get; set; }

        [Required]
        public string? place { get; set; }
    }
}
