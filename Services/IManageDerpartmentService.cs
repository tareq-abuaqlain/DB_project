using Cassandra;
using Npgsql;
using NuGet.Protocol.Plugins;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageDerpartmentService
    {
        List<Department>? GetAllDepartments();
        Department? GetDepartmentsByName(string dept_name);
        bool AddDepartment(Department department);
        bool DeleteDepartment(string dept_name);
        bool UpdateDepartment(Department department);

    }


    public class ManageDerpartmentService : IManageDerpartmentService
    {
        private readonly NpgsqlConnection _context;

        public ManageDerpartmentService()
        {
            _context = (new ContextDB()).connect();
            _context.Close();
        }
        public bool AddDepartment(Department department)
        {
            
            if (department != null)
            {
                var dept = GetDepartmentsByName(department.dept_name??"");
                if (dept == null)
                {
                    _context.Close();
                    _context.Open();
                    string query = "insert into Department values (@x,@y,@z)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                    {
                        cmd.Parameters.AddWithValue("x", department.dept_name ?? "");
                        cmd.Parameters.AddWithValue("y", department.building ?? "");
                        cmd.Parameters.AddWithValue("z", department.budget);
                        int reader = cmd.ExecuteNonQuery();
                        _context.Close();
                        return (reader == -1) ? false : true;
                    }
                }
            }
            return false;
            
        }

        public bool DeleteDepartment(string dept_name)
        {
            
            var dept = GetDepartmentsByName(dept_name);
            if (dept != null)
            {
                _context.Close();
                _context.Open();
                string query = "delete from Department where dept_name = @x";
                NpgsqlCommand cmd = new NpgsqlCommand(query, _context);
                cmd.Parameters.AddWithValue("x", dept_name ?? "");
                int reader = cmd.ExecuteNonQuery();
                _context.Close();
                return (reader == -1) ? false : true;
            }
            return false;
        }

        public List<Department>? GetAllDepartments()
        {
            _context.Close();
            _context.Open();
            List<Department>? departmentList = null ;
            string query = "select * from department";
            NpgsqlCommand cmd = new NpgsqlCommand(query, _context);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                departmentList = new List<Department>();
                while (reader.Read())
                {

                    Department department = new Department();
                    department.dept_name = reader.GetString(0);
                    department.building = reader.GetString(1);
                    department.budget = reader.GetInt32(2);
                    departmentList.Add(department);
                }
            }
            _context.Close();
            return departmentList;
        }

        public Department? GetDepartmentsByName(string dept_name)
        {
            _context.Close ();
            _context.Open();
            Department? department = null;
            string query = "select * from Department where dept_name = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", dept_name??"");
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    department = new Department();
                    department.dept_name = reader.GetString(0);
                    department.building = reader.GetString(1);
                    department.budget = reader.GetInt32(2);
                }
            }
            _context.Close();
            return department;
        }

        public bool UpdateDepartment(Department department)
        {
            Department? dept = GetDepartmentsByName(department!.dept_name??"");
            if (department != null)
            {
                _context.Close();
                _context.Open();
                string query = "update Department set building = @x , budget = @y where dept_name = @z";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", department.building ?? "");
                    cmd.Parameters.AddWithValue("y", department.budget);
                    cmd.Parameters.AddWithValue("z", department.dept_name ?? "");
                    int  reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;  
                }
               
            }
            return false;
        }
    }
}
