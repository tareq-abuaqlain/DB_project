using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace ProjectDB.Data
{
    public class ContextDB
    {
 
        public NpgsqlConnection connect()
        {
            string DDLPath = "DDL_new.sql";
            DDLPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//"+DDLPath));
            string DDL = File.ReadAllText(DDLPath);
            
            string DataPath = "Data.sql";
            DataPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//"+DataPath));
            string Data = File.ReadAllText(DataPath);

            string connString = "Server=localhost;Port=5432;Database=ProjectDB;username=postgres;password=root";
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();
            using(NpgsqlCommand cmd = new NpgsqlCommand(DDL, conn))
            {
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("DDL file executed successfully.");
            
            using(NpgsqlCommand cmd = new NpgsqlCommand(Data, conn))
            {
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Data file executed successfully.");
            conn.Close();
            return conn;
        }


    }
}
