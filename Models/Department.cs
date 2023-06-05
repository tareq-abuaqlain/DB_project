using System.ComponentModel.DataAnnotations;

namespace ProjectDB.Models
{
    public class Department
    {
        [Key]
        public string? dept_name { get; set; }

        [Required]
        public string? building { get; set; }
        [Required]
        public double budget { get; set; }
    }
}
