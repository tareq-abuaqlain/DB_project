using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;
using System.Text;

namespace ProjectDB.Services
{
    public interface IManageCourseService
    {
        List<Course>? GetAllCourses();
        List<Course>? GetAllCoursesByDept_name(string dept_name);
        Course? GetCourseById(int course_id);
        bool AddCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(int course_id);

    }

    class ManageCourseService : IManageCourseService
    {
        private readonly NpgsqlConnection _context;
        private readonly IManageDerpartmentService derpartmentService;

        public ManageCourseService(IManageDerpartmentService derpartmentService)
        {
            _context = (new ContextDB()).connect();
            _context.Close();
            this.derpartmentService = derpartmentService;
        }
        public bool AddCourse(Course course)
        {
            _context.Close();
            _context.Open();
            string query = "insert into course (title , dept_name , credits)" +
                "values (@a,@b,@c)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("a", course.title?? "");
                cmd.Parameters.AddWithValue("b", course.department!.dept_name??"");
                cmd.Parameters.AddWithValue("c", course.credits);
                int reader = cmd.ExecuteNonQuery();
                _context.Close();
                return (reader == -1) ? false : true;
            }
        }

        public bool DeleteCourse(int course_id)
        {
            var course = GetCourseById(course_id);
            if(course != null)
            {
                _context.Close();
                _context.Open();
                string query = "delete from course where course_id = @x";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", course_id);
                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            _context.Close();
            return false;
        }

        public List<Course>? GetAllCourses()
        {
            _context.Close();
            _context.Open();
            List<Course>? courses = null;
            string query = "select * from course order by course_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader != null)
                {
                    courses = new List<Course>();
                    while (reader.Read())
                    {
                        var course = new Course();
                        course.course_Id = reader.GetInt32(0);
                        course.title = reader.GetString(1);
                        course.department = derpartmentService.GetDepartmentsByName(reader.GetString(2));
                        course.credits = reader.GetInt32(3);
                        courses.Add(course);
                    }
                    _context.Close();
                    return courses;
                }
                _context.Close();
                return null;
            }
        }

        public List<Course>? GetAllCoursesByDept_name(string dept_name)
        {
            _context.Close();
            _context.Open();
            List<Course>? courses = null;
            string query = "select * from course where dept_name = @x order by course_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", dept_name ?? "");
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    courses = new List<Course>();
                    while (reader.Read())
                    {
                        var course = new Course();
                        course.course_Id = reader.GetInt32(0);
                        course.title = reader.GetString(1);
                        course.department = derpartmentService.GetDepartmentsByName(reader.GetString(2));
                        course.credits = reader.GetInt32(3);
                        courses.Add(course);
                    }
                    _context.Close();
                    return courses;
                }
                _context.Close();
                return null;
            }
        }

        public Course? GetCourseById(int course_id)
        {
            _context.Close();
            _context.Open();
            Course? course = null;
            string query = "select * from course where course_id = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", course_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    course = new Course();
                    course.course_Id = reader.GetInt32(0);
                    course.title = reader.GetString(1);
                    course.department = derpartmentService.GetDepartmentsByName(reader.GetString(2));
                    course.credits = reader.GetInt32(3);
                }
                _context.Close();
                return course;
            }
        }

        public bool UpdateCourse(Course course)
        {
            var course_temp = GetCourseById(course.course_Id);
            if (course_temp != null)
            {
                _context.Close();
                _context.Open();
                string query = "update course set title = @a,dept_name = @b,credits = @c " +
                    "where course_id = @d";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("a", course.title ?? "");
                    cmd.Parameters.AddWithValue("b", course.department!.dept_name ?? "");
                    cmd.Parameters.AddWithValue("c", course.credits);
                    cmd.Parameters.AddWithValue("d", course.course_Id);
                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            _context.Close();
            return false;
        }
    }
}
