namespace ProjectDB.Models
{
    public class Lecture
    {
        public int lecture_id { get; set; }
        public Div? div { get; set; }
        public DateTime time { get; set; }
        public string? title { get; set; }
    }
}
