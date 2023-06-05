using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageDivService
    {
        List<Div>? GetAllDivs();
        List<Div>? GetAllDivsByUsername(string username);
        List<Div>? GetAllDivsByCourseId(int course_id);
        List<Div>? GetAllDivsByInstructorId(int inst_id);
        List<Div>? GetAllDivsByCourseTeach(int course_id ,int inst_id);
        Div? GetDivById(int div_id);
        bool AddDiv(Div div);
        bool UpdateDiv(Div div);
        bool DeleteDiv(int div_id);


    }

    class ManageDivService : IManageDivService
    {
        private readonly NpgsqlConnection _context;
        private readonly IManageCourseService courseService;
        private readonly IManageInstructorService instructorService;
        private readonly IManageUserServices userService;

        public ManageDivService(
            IManageCourseService courseService,
            IManageInstructorService instructorService,
            IManageUserServices userService)
        {
            _context = (new ContextDB()).connect();
            _context.Close();
            this.courseService = courseService;
            this.instructorService = instructorService;
            this.userService = userService;
        }

        public bool AddDiv(Div div)
        {

            if (div != null)
            {
                _context.Close();
                _context.Open();
                string query = "insert into div" +
                    "(course_id,inst_id,user_id,room,day,time,type)" +
                    "values (@a,@b,@c,@d,@e,@f,@g)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("a",div.course!.course_Id);
                    cmd.Parameters.AddWithValue("b",div.instructor!.inst_id);
                    cmd.Parameters.AddWithValue("c",div.user!.user_id);
                    cmd.Parameters.AddWithValue("d",div.room);
                    cmd.Parameters.AddWithValue("e",div.day);
                    cmd.Parameters.AddWithValue("f",div.time);
                    cmd.Parameters.AddWithValue("g",(int)div.type);
                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }

        public bool DeleteDiv(int div_id)
        {
            var div = GetDivById(div_id);
            if (div != null)
            {
                _context.Close();
                _context.Open();
                string query = "delete from div where div_id = @x";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", div_id);
                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }

        public List<Div>? GetAllDivs()
        {
            _context.Close();
            _context.Open();
            List<Div>? divs = null;
            string query = "select * from div order by div_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    divs = new List<Div>();
                    while (reader.Read())
                    {
                        var div = new Div();
                        div.div_id = reader.GetInt32(0);
                        div.course = courseService.GetCourseById(reader.GetInt32(1));
                        div.instructor = instructorService.GetInstructorById(reader.GetInt32(2));
                        div.user = userService.GetUserById(reader.GetInt32(3));
                        div.room = reader.GetInt32(4);
                        div.day = reader.GetInt32(5);
                        div.time = reader.GetTimeSpan(6);
                        div.type = (typePerson)Enum.ToObject(typeof(typePerson), reader.GetInt32(7));
                        divs.Add(div);
                    }
                }
                _context.Close();
                return divs;
            }
        }

        public List<Div>? GetAllDivsByCourseId(int course_id)
        {
            _context.Close();
            _context.Open();
            List<Div>? divs = null;
            string query = "select * from div wher course_id = @x order by div_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", course_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    divs = new List<Div>();
                    while (reader.Read())
                    {
                        var div = new Div();
                        div.div_id = reader.GetInt32(0);
                        div.course = courseService.GetCourseById(reader.GetInt32(1));
                        div.instructor = instructorService.GetInstructorById(reader.GetInt32(2));
                        div.user = userService.GetUserById(reader.GetInt32(3));
                        div.room = reader.GetInt32(4);
                        div.day = reader.GetInt32(5);
                        div.time = reader.GetTimeSpan(6);
                        div.type = (typePerson)Enum.ToObject(typeof(typePerson), reader.GetInt32(7));
                        divs.Add(div);
                    }
                }
                _context.Close();
                return divs;
            }
        }

        public List<Div>? GetAllDivsByCourseTeach(int course_id, int inst_id)
        {
            _context.Close();
            _context.Open();
            List<Div>? divs = null;
            string query = "select * from div where course_id = @x and inst_id = @y order by div_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", course_id);
                cmd.Parameters.AddWithValue("y", inst_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    divs = new List<Div>();
                    while (reader.Read())
                    {
                        var div = new Div();
                        div.div_id = reader.GetInt32(0);
                        div.course = courseService.GetCourseById(reader.GetInt32(1));
                        div.instructor = instructorService.GetInstructorById(reader.GetInt32(2));
                        div.user = userService.GetUserById(reader.GetInt32(3));
                        div.room = reader.GetInt32(4);
                        div.day = reader.GetInt32(5);
                        div.time = reader.GetTimeSpan(6);
                        div.type = (typePerson)Enum.ToObject(typeof(typePerson), reader.GetInt32(7));
                        divs.Add(div);
                    }
                }
                _context.Close();
                return divs;
            }
        }

        public List<Div>? GetAllDivsByInstructorId(int inst_id)
        {
            _context.Close();
            _context.Open();
            List<Div>? divs = null;
            string query = "select * from div wher inst_id = @x order by div_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", inst_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    divs = new List<Div>();
                    while (reader.Read())
                    {
                        var div = new Div();
                        div.div_id = reader.GetInt32(0);
                        div.course = courseService.GetCourseById(reader.GetInt32(1));
                        div.instructor = instructorService.GetInstructorById(reader.GetInt32(2));
                        div.user = userService.GetUserById(reader.GetInt32(3));
                        div.room = reader.GetInt32(4);
                        div.day = reader.GetInt32(5);
                        div.time = reader.GetTimeSpan(6);
                        div.type = (typePerson)Enum.ToObject(typeof(typePerson), reader.GetInt32(7));
                        divs.Add(div);
                    }
                }
                _context.Close();
                return divs;
            }
        }

        public List<Div>? GetAllDivsByUsername(string username)
        {
            _context.Close();
            _context.Open();
            List<Div>? divs = null;
            var user = userService.GetUserByUserName(username);
            string query = "select * from div where user_id = @x";
            using(NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", user!.user_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader != null)
                {
                    divs = new List<Div>();
                    while(reader.Read())
                    {
                        var div = new Div();
                        div.div_id = reader.GetInt32(0);
                        div.course = courseService.GetCourseById(reader.GetInt32(1));
                        div.instructor = instructorService.GetInstructorById(reader.GetInt32(2));
                        div.user = userService.GetUserById(reader.GetInt32(3));
                        div.room = reader.GetInt32(4);
                        div.day = reader.GetInt32(5);
                        div.time = reader.GetTimeSpan(6);
                        div.type = (typePerson)reader.GetInt32(7);
                        divs.Add(div);
                    }
                }
                _context.Close();
                return divs;
            }
        }

        public Div? GetDivById(int div_id)
        {
            _context.Close();
            _context.Open();
            Div? div = null;
            string query = "select * from div where div_id = @a";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("a", div_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    div = new Div();
                    div.div_id = reader.GetInt32(0);
                    div.course = courseService.GetCourseById(reader.GetInt32(1));
                    div.instructor = instructorService.GetInstructorById(reader.GetInt32(2));
                    div.user = userService.GetUserById(reader.GetInt32(3));
                    div.room = reader.GetInt32(4);
                    div.day = reader.GetInt32(5);
                    div.time = reader.GetTimeSpan(6);
                    div.type = (typePerson)Enum.ToObject(typeof(typePerson),reader.GetInt32(7));
                }
                _context.Close();
                return div;
            }
        }

        public bool UpdateDiv(Div div)
        {
            if (div != null)
            {
                var div_temp = GetDivById(div.div_id);
                if(div_temp != null)
                {
                    _context.Close();
                    _context.Open();
                    string query = "update div set user_id = @a," +
                        "room = @b , day = @c, time = @d, type = @e where div_id = @f";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                    {
                        cmd.Parameters.AddWithValue("a", div.user!.user_id);
                        cmd.Parameters.AddWithValue("b", div.room);
                        cmd.Parameters.AddWithValue("c", div.day);
                        cmd.Parameters.AddWithValue("d", div.time);
                        cmd.Parameters.AddWithValue("e", (int)div.type);
                        cmd.Parameters.AddWithValue("f", div.div_id);
                        int reader = cmd.ExecuteNonQuery();
                        _context.Close();
                        return (reader == -1) ? false : true;
                    }
                }
            }
            return false;
        }
    }
}
