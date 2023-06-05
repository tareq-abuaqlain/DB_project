using System.ComponentModel.DataAnnotations;

namespace ProjectDB.Models
{
    public class Instructor
    {
        [Key]
        public int inst_id { get; set; }
        [Required]
        public string? name { get; set; }

        [Required]
        public Department? department { get; set; }

        [Required]
        public double salary { get; set; }
    }
}
