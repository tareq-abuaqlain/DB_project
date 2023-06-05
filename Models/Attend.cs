namespace ProjectDB.Models
{
    public class Attend
    {
        public Lecture? lecture { get; set; }
        public Student? student { get; set; }
        public bool isAttend { get; set; }
    } 
}
