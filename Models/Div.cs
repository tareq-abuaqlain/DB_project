namespace ProjectDB.Models
{
    public class Div
    {
        public int div_id { get; set; }
        public Course? course { get; set; }
        public Instructor? instructor { get; set; }
        public User? user { get; set; }
        public int room { get; set; }
        public int day { get; set; }
        public TimeSpan time { get; set; }
        public typePerson type { get; set; }
    }

    public enum typePerson
    {
        Male,
        Female
    } 
}
