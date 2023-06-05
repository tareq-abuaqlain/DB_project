using NuGet.Packaging.Signing;

namespace ProjectDB.Models
{
    public class Registration
    {
        public Student? student { get; set; }
        public Course? course { get; set; }
        public Div? div { get; set; }
        public DateTime? date { get; set; }
    }
}
