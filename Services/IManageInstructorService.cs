using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageInstructorService
    {
        List<Instructor>? GetAllInstructors();
        Instructor? GetInstructorById(int inst_id);
        List<Instructor>? GetInstructorByName(string inst_name);
        bool AddInstructor(Instructor instructor);
        bool UpdateInstructor(Instructor instructor);
        bool DeleteInstructor(int inst_id);
    }

    public class ManageInstructorService : IManageInstructorService
    {
        private readonly NpgsqlConnection _context;
        private readonly IManageDerpartmentService _Dept_context;

        public ManageInstructorService(IManageDerpartmentService dept_context)
        {
            _context = (new ContextDB()).connect();
            _context.Close();
            _Dept_context = dept_context;
        }


        public bool AddInstructor(Instructor instructor)
        {
            if (instructor != null && instructor.department != null)
            {
                _context.Close();
                _context.Open();
                string query = "insert into instructor" +
                    "(name,dept_name,salary) values (@x,@y,@z)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    //cmd.Parameters.AddWithValue("w", 0);
                    cmd.Parameters.AddWithValue("x", instructor.name??"");
                    cmd.Parameters.AddWithValue("y", instructor.department.dept_name??"");
                    cmd.Parameters.AddWithValue("z", instructor.salary);

                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1)?false:true;
                }
            }
            return false;
        }

        public bool DeleteInstructor(int inst_id)
        {
            Instructor? inst = GetInstructorById(inst_id);
            if(inst != null) {
                _context.Close();
                _context.Open();
                string query = "delete from instructor where inst_id = @x";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", inst_id);

                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }

        public List<Instructor>? GetAllInstructors()
        {
            _context.Close();
            _context.Open();
            List<Instructor>? instructors = null;
            string query = "select * from instructor";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader != null)
                {
                    instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        var inst = new Instructor();
                        inst.inst_id = reader.GetInt32(0);
                        inst.name = reader.GetString(1);
                        inst.department = _Dept_context.GetDepartmentsByName(reader.GetString(2));
                        inst.salary = reader.GetDouble(3);
                        instructors.Add(inst);
                    }
                    _context.Close();
                    return instructors;
                }
                _context.Close();
                return null;
            }
        }

        public Instructor? GetInstructorById(int inst_id)
        {
            _context.Close();
            _context.Open();
            Instructor? inst = null;
            string query = "select * from instructor where inst_id = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", inst_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader != null)
                {
                    while(reader.Read())
                    {
                        inst = new Instructor();
                        inst.inst_id = reader.GetInt32(0);
                        inst.name = reader.GetString(1);
                        inst.department = _Dept_context.GetDepartmentsByName(reader.GetString(2));
                        inst.salary = reader.GetDouble(3);

                    }
                    _context.Close();
                    return inst;
                }
                _context.Close();
                return null;
            }
        }

        public List<Instructor> GetInstructorByName(string inst_name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateInstructor(Instructor instructor)
        {
            if (instructor != null && instructor.department != null)
            {
                _context.Close();
                _context.Open();
                string query = "update instructor set name = @x , dept_name = @y ,salary = @z where inst_id = @w";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", instructor.name ?? "");
                    cmd.Parameters.AddWithValue("y", instructor.department.dept_name ?? "");
                    cmd.Parameters.AddWithValue("z", instructor.salary);
                    cmd.Parameters.AddWithValue("w", instructor.inst_id);

                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }
    }
}
