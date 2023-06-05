using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;
using System.Security.Principal;

namespace ProjectDB.Services
{
    public interface IManageStudentsService
    {
        List<Student>? GetAllStudents();
        Student? GetStudent(int std_id);
        bool AddStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(int std_id);
    }

    class ManageStudentsService : IManageStudentsService
    {
        private readonly NpgsqlConnection _context;
        private readonly IManageDerpartmentService derpartmentService;

        public ManageStudentsService(IManageDerpartmentService derpartmentService)
        {
            _context = (new ContextDB()).connect();
            this.derpartmentService = derpartmentService;
            _context.Close();
        }
        
        private bool checkStdId(int std_id)
        {
            string id = std_id.ToString();
            
            if(!string.IsNullOrEmpty(id) && id.Length ==9  )
            {
                int year = int.Parse(id.Substring(1, 4));
                if ( ( int.Parse( id[0]+"") == 1 || int.Parse(id[0] + "") == 2)
                    && year >= 1978 && year <= DateTime.Now.Year)
                {
                    return true;
                }

            }
            return false;
        }
        public bool AddStudent(Student student)
        {
            var studentTemp = GetStudent(student.std_id);
            if (studentTemp == null && student != null && checkStdId(student.std_id))
            {
                _context.Close();
                _context.Open();
                string query = "insert into student  " +
                    " values (@a,@b,@c,@d)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("a", student.std_id);
                    cmd.Parameters.AddWithValue("b",student.name??"" );
                    cmd.Parameters.AddWithValue("c", student.department!.dept_name??"");
                    cmd.Parameters.AddWithValue("d", student.place??"");
                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            _context.Close();
            return false;
        }

        public bool DeleteStudent(int std_id)
        {
            var student = GetStudent(std_id);
            if(student != null)
            {
                _context.Close();
                _context.Open();
                string query = "delete from student where std_id = @x";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", std_id) ;
                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            _context.Close();
            return false;
        }

        public List<Student>? GetAllStudents()
        {
            _context.Close();
            _context.Open();
            List<Student>? students = null;
            string query = "select * from student order by std_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    students = new List<Student>();
                    while (reader.Read())
                    {
                        var student = new Student();
                        student.std_id = reader.GetInt32(0);
                        student.name = reader.GetString(1);
                        student.department = derpartmentService.GetDepartmentsByName(reader.GetString(2));
                        student.place = reader.GetString(3);
                        students.Add(student);
                    }
                }
                _context.Close();
                return students;
            }
        }

        public Student? GetStudent(int std_id)
        {
            _context.Close();
            _context.Open();
            Student? student = null;
            string query = "select * from student where std_id = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", std_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    student = new Student();
                    student.std_id = reader.GetInt32(0);
                    student.name = reader.GetString(1);
                    student.department = derpartmentService.GetDepartmentsByName(reader.GetString(2));
                    student.place = reader.GetString(3);
                }
                _context.Close();
                return student;
            }
        }


        public bool UpdateStudent(Student student)
        {
            var studentTemp = GetStudent(student.std_id);
            if (studentTemp != null && student != null)
            {
                _context.Close();
                _context.Open();
                string query = "update student set place = @x where std_id = @y";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", student.place ?? "");
                    cmd.Parameters.AddWithValue("y", student.std_id);
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
