using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageLecturesService
    {
        bool AddLecture(Lecture lecture);
        List<Lecture>? GetAllLecturesByDiv(int div_id);
        Lecture? GetLecture(int lecture_id);
    }

    class ManageLecturesService : IManageLecturesService
    {
        private readonly NpgsqlConnection service;
        private readonly IManageDivService divService;

        public ManageLecturesService(IManageDivService divService)
        {
            service = (new ContextDB()).connect();
            this.divService = divService;
        }
        public bool AddLecture(Lecture lecture)
        {
            if(lecture != null)
            {
                service.Close();
                service.Open();
                string query = "insert into lecture (div_id,time,title) " +
                    "values (@a,@b,@c)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query,service))
                {
                    cmd.Parameters.AddWithValue("a",lecture.div!.div_id);
                    cmd.Parameters.AddWithValue("b",lecture.time);
                    cmd.Parameters.AddWithValue("c",lecture.title??"");

                    int reader = cmd.ExecuteNonQuery();
                    service.Close();
                    return (reader == -1) ? false : true;
                }
            }
            service.Close();
            return false;
        }

        public List<Lecture>? GetAllLecturesByDiv(int div_id)
        {
            service.Close();
            service.Open ();
            List<Lecture>? lectures = null;
            string query = "select * from lecture where div_id = @x";
            using(NpgsqlCommand cmd = new NpgsqlCommand(query, service))
            {
                cmd.Parameters.AddWithValue("x", div_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader != null)
                {
                    lectures = new List<Lecture>();
                    while(reader.Read())
                    {
                        var lec = new Lecture();
                        lec.lecture_id = reader.GetInt32(0);
                        lec.div = divService.GetDivById(reader.GetInt32(1));
                        lec.time = reader.GetDateTime(2);
                        lec.title = reader.GetString(3);
                        lectures.Add(lec);
                    }
                }
                service.Close();
                return lectures;
            }
        }

        public Lecture? GetLecture(int lecture_id)
        {
            service.Close();
            service.Open();
            Lecture? lecture = null;
            string query = "select * from lecture where lecture_id = @x";
            using(NpgsqlCommand cmd = new NpgsqlCommand(query, service))
            {
                cmd.Parameters.AddWithValue("x", lecture_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lecture = new Lecture();
                    lecture.lecture_id = reader.GetInt32(0);
                    lecture.div = divService.GetDivById(reader.GetInt32(1));
                    lecture.time = reader.GetDateTime(2);
                    lecture.title = reader.GetString(3);
                }
                service.Close();
                return lecture;
            }
        }
    }
}
