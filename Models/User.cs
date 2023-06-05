namespace ProjectDB.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public int identity { get; set; }
        public DateOnly date { get; set; } 
    }
}

