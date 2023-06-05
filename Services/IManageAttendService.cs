using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageAttendService
    {
        bool AddAttend(Attend attend);
        bool UpdateAttend(Attend attend);
        Attend? GetAttend(int std_id ,int lecture_id );

        bool isLectureFound(int lecture_id);
    }

    class ManageAttendService : IManageAttendService
    {
        private readonly NpgsqlConnection service;
        private readonly IManageStudentsService studentsService;
        private readonly IManageLecturesService lecturesService;

        public ManageAttendService(IManageStudentsService studentsService, IManageLecturesService lecturesService)
        {
            service = (new ContextDB()).connect();
            this.studentsService = studentsService;
            this.lecturesService = lecturesService;
        }

        public bool AddAttend(Attend attend)
        {
            var att = GetAttend(attend.student!.std_id, attend.lecture!.lecture_id);
            if (att == null)
            {
                service.Close();
                service.Open();
                string query = "insert into attend values (@a,@b,@c)";
                using(NpgsqlCommand cmd = new NpgsqlCommand(query, service))
                {
                    cmd.Parameters.AddWithValue("b",attend.student!.std_id);
                    cmd.Parameters.AddWithValue("a",attend.lecture!.lecture_id);
                    cmd.Parameters.AddWithValue("c",attend.isAttend);

                    int reader = cmd.ExecuteNonQuery();
                    service.Close();
                    return (reader == -1)?false:true;
                }
            }
            service.Close();
            return false;
        }

        public Attend? GetAttend(int std_id, int lecture_id)
        {
            service.Close();
            service.Open();
            Attend? attend = null;
            string query = "select * from attend where std_id = @x and lecture_id = @y";
            using(NpgsqlCommand cmd = new NpgsqlCommand(query, service))
            {
                cmd.Parameters.AddWithValue("x",std_id);
                cmd.Parameters.AddWithValue("y",lecture_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    attend = new Attend();
                    attend.lecture = lecturesService.GetLecture(reader.GetInt32(0));
                    attend.student = studentsService.GetStudent(reader.GetInt32(1));
                    attend.isAttend = reader.GetBoolean(2);
                }
                service.Close();
                return attend;
            }
        }

        public bool isLectureFound(int lecture_id)
        {
            service.Close();
            service.Open();
            string query = "select * from attend where lecture_id = @a";
            using(NpgsqlCommand cmd = new NpgsqlCommand(query,service))
            {
                cmd.Parameters.AddWithValue("a",lecture_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    service.Close();
                    return false;
                }
                else
                {
                    service.Close();
                    return true;
                }
            }
        }

        public bool UpdateAttend(Attend attend)
        {
            var att = GetAttend(attend.student!.std_id, attend.lecture!.lecture_id);
            if(att != null)
            {
                service.Close();
                service.Open();
                string query = "update attend set isattend = @a where lecture_id = @b and std_id = @c";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, service))
                {
                    cmd.Parameters.AddWithValue("a", attend.isAttend);
                    cmd.Parameters.AddWithValue("b",att.lecture!.lecture_id);
                    cmd.Parameters.AddWithValue("c",att.student!.std_id);

                    int reader = cmd.ExecuteNonQuery();
                    service.Close();
                    return (reader == -1)?false :true;
                }
            }
            service.Close() ;
            return false;
        }
    }
}
