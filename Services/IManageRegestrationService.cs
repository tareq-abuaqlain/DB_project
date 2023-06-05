using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageRegestrationService
    {
        bool RegisterStudent(int std_id , int course_id , int div_id);
        Registration? GetRegistration(int std_id, int course_id);
        List<Student>? GetAllStudentsInDiv(int div_id);
    }

    class ManageRegestrationService : IManageRegestrationService
    {
        private readonly NpgsqlConnection service;
        private readonly IManageDivService divService;
        private readonly IManageStudentsService studentsService;
        private readonly IManageCourseService courseService;

        public ManageRegestrationService(
            IManageDivService divService,
            IManageStudentsService studentsService,
            IManageCourseService courseService)
        {
            service = (new ContextDB()).connect();
            service.Close();
            this.divService = divService;
            this.studentsService = studentsService;
            this.courseService = courseService;
        }

        public List<Student>? GetAllStudentsInDiv(int div_id)
        {
            var div = divService.GetDivById(div_id);
            List<Student>? students = null;
            if (div != null )
            {
                service.Close();
                service.Open();
                string query = "select std_id from registration where div_id = @x";
                using(NpgsqlCommand cmd = new NpgsqlCommand(query,service))
                {
                    cmd.Parameters.AddWithValue("x", div_id);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if(reader  != null)
                    {
                        students = new List<Student>();
                        while(reader.Read())
                        {
                            var student = studentsService.GetStudent(reader.GetInt32(0));
                            if(student != null)
                                students.Add(student);
                        }
                    }
                    service.Close();
                    return students;
                }
            }
            service.Close();
            return students;
        }

        public Registration? GetRegistration(int std_id, int course_id)
        {
            service.Close();
            service.Open();
            Registration? reg = null;
            string query = "Select * from registration where std_id =@x and course_id = @y";
            using(NpgsqlCommand cmd = new NpgsqlCommand(query, service))
            {
                cmd.Parameters.AddWithValue("x", std_id);
                cmd.Parameters.AddWithValue("y", course_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reg = new Registration();
                    reg.student = studentsService.GetStudent(reader.GetInt32(0));
                    reg.course = courseService.GetCourseById(reader.GetInt32(1));
                    reg.div = divService.GetDivById(reader.GetInt32(2));
                    reg.date = reader.GetDateTime(3);
                }
                service.Close();
                return reg;
            }
        }

        public bool RegisterStudent(int std_id, int course_id, int div_id)
        {
            var div = divService.GetDivById(div_id);
            if(div != null 
                && ( 
                (div.type == typePerson.Male && (std_id + "")[0].Equals("1")) || 
                (div.type == typePerson.Female && (std_id + "")[0].Equals("2"))  
                ) )
            {
                var reg = GetRegistration(std_id, course_id);
                if(div.course!.course_Id == course_id && reg == null)
                {
                    service.Close();
                    service.Open();
                    string query = "insert into registration values (@a,@b,@c,@d)";
                    using(NpgsqlCommand cmd = new NpgsqlCommand(query, service))
                    {
                        cmd.Parameters.AddWithValue("a",std_id);
                        cmd.Parameters.AddWithValue("b",course_id);
                        cmd.Parameters.AddWithValue("c",div_id);
                        cmd.Parameters.AddWithValue("d",DateTime.Now);

                        int reader = cmd.ExecuteNonQuery();
                        service.Close();
                        return (reader == -1) ? false : true;
                    }
                }
            }
            service.Close();
            return false;
        }
    }
}
