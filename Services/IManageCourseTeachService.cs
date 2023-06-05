using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageCourseTeachService
    {
        List<CourseTeach>? GetAllCourseTeachs();
        List<CourseTeach>? GetAllCourseTeachsByCourseId(int course_id);
        List<CourseTeach>? GetAllCourseTeachsByInstructorId(int inst_id);
        CourseTeach? GetCourseTeach(int course_id, int inst_id);
        bool AddCourseTeach(CourseTeach courseTeach);
        bool UpdateCourseTeach(CourseTeach courseTeach);
        bool DeleteCourseTeach(int course_id , int inst_id);
    }

    class ManageCourseTeachService : IManageCourseTeachService
    {

        private readonly NpgsqlConnection _context;
        private readonly IManageDerpartmentService derpartmentService;
        private readonly IManageCourseService courseService;
        private readonly IManageInstructorService instServices;

        public ManageCourseTeachService(
            IManageDerpartmentService derpartmentService,
            IManageCourseService courseService,
            IManageInstructorService instServices
            ){
            _context = (new ContextDB()).connect();
            _context.Close();
            this.derpartmentService = derpartmentService;
            this.courseService = courseService;
            this.instServices = instServices;
        }

        public bool AddCourseTeach(CourseTeach courseTeach)
        {
            if (courseTeach != null)
            {
                var courseTeachTemp = GetCourseTeach(courseTeach.course!.course_Id, courseTeach.instructor!.inst_id);
                if(courseTeachTemp == null)
                {
                    _context.Close();
                    _context.Open();
                    string query = "insert into course_teach values (@a,@b,@c,@d)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                    {
                        cmd.Parameters.AddWithValue("a", courseTeach.course.course_Id);
                        cmd.Parameters.AddWithValue("b", courseTeach.instructor.inst_id);
                        cmd.Parameters.AddWithValue("c", courseTeach.topic ??"");
                        cmd.Parameters.AddWithValue("d", courseTeach.book ??"");
                        int reader = cmd.ExecuteNonQuery();
                        _context.Close();
                        return (reader == -1) ? false : true;
                    }
                }
            }
            _context.Close();
            return false;
        }

        public bool DeleteCourseTeach(int course_id, int inst_id)
        {
            var courseTeachTemp = GetCourseTeach(course_id, inst_id);
            if (courseTeachTemp != null)
            {
                _context.Close();
                _context.Open();
                string query = "delete from course_teach where course_id = @a and inst_id = @b";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("a", course_id);
                    cmd.Parameters.AddWithValue("b", inst_id);
                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            _context.Close ();
            return false;
        }

        public List<CourseTeach>? GetAllCourseTeachs()
        {
            _context.Close();
            _context.Open();
            List<CourseTeach>? courseTeachs = null;
            string query = "select * from course_teach order by course_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    courseTeachs = new List<CourseTeach> ();
                    while (reader.Read())
                    {
                        var courseTeach = new CourseTeach();
                        courseTeach.course = courseService.GetCourseById(reader.GetInt32(0));
                        courseTeach.instructor = instServices.GetInstructorById(reader.GetInt32(1));
                        courseTeach.topic = reader.GetString(2);
                        courseTeach.book = reader.GetString(3);
                        courseTeachs.Add(courseTeach);
                    }
                }
                _context.Close();
                return courseTeachs;
            }
        }
        public List<CourseTeach>? GetAllCourseTeachsByCourseId(int course_id)
        {
            _context.Close();
            _context.Open();
            List<CourseTeach>? courseTeachs = null;
            string query = "select * from course_teach where course_id = @x order by course_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", course_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    courseTeachs = new List<CourseTeach>();
                    while (reader.Read())
                    {
                        var courseTeach = new CourseTeach();
                        courseTeach.course = courseService.GetCourseById(reader.GetInt32(0));
                        courseTeach.instructor = instServices.GetInstructorById(reader.GetInt32(1));
                        courseTeach.topic = reader.GetString(2);
                        courseTeach.book = reader.GetString(3);
                        courseTeachs.Add(courseTeach);
                    }
                }
                _context.Close();
                return courseTeachs;
            }
        }

        public List<CourseTeach>? GetAllCourseTeachsByInstructorId(int inst_id)
        {
            _context.Close();
            _context.Open();
            List<CourseTeach>? courseTeachs = null;
            string query = "select * from course_teach where inst_id = @x order by inst_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", inst_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    courseTeachs = new List<CourseTeach>();
                    while (reader.Read())
                    {
                        var courseTeach = new CourseTeach();
                        courseTeach.course = courseService.GetCourseById(reader.GetInt32(0));
                        courseTeach.instructor = instServices.GetInstructorById(reader.GetInt32(1));
                        courseTeach.topic = reader.GetString(2);
                        courseTeach.book = reader.GetString(3);
                        courseTeachs.Add(courseTeach);
                    }
                }
                _context.Close();
                return courseTeachs;
            }
        }

        public CourseTeach? GetCourseTeach(int course_id, int inst_id)
        {
            _context.Close();
            _context.Open();
            CourseTeach? courseTeach = null;
            string query = "select * from course_teach where course_id = @a and inst_id = @b";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("a", course_id);
                cmd.Parameters.AddWithValue("b", inst_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    courseTeach = new CourseTeach();
                    courseTeach.course = courseService.GetCourseById(reader.GetInt32(0));
                    courseTeach.instructor = instServices.GetInstructorById(reader.GetInt32(1));
                    courseTeach.topic = reader.GetString(2);
                    courseTeach.book = reader.GetString(3);
                }
                _context.Close();
                return courseTeach;
            }
        }

        public bool UpdateCourseTeach(CourseTeach courseTeach)
        {
            
            if (courseTeach != null)
            {
                var courseTeachTemp = GetCourseTeach(courseTeach.course!.course_Id, courseTeach.instructor!.inst_id);
                if (courseTeachTemp != null)
                {
                    _context.Close();
                    _context.Open();
                    string query = "update course_teach set topic = @a , book = @b where " +
                        "course_id = @c and inst_id = @d";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                    {
                        cmd.Parameters.AddWithValue("c", courseTeach.course.course_Id);
                        cmd.Parameters.AddWithValue("d", courseTeach.instructor.inst_id);
                        cmd.Parameters.AddWithValue("a", courseTeach.topic ?? "");
                        cmd.Parameters.AddWithValue("b", courseTeach.book ?? "");
                        int reader = cmd.ExecuteNonQuery();
                        _context.Close();
                        return (reader == -1) ? false : true;
                    }
                }
            }
            _context.Close();
            return false;
        }
    }
}
