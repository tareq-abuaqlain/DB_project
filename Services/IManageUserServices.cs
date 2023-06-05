using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProjectDB.Services
{
    public interface IManageUserServices
    {
        List<User>? GetAllUsers();
        List<User>? GetAllUsersByRole(string roleName);
        User? GetUserById(int user_id);
        User? GetUserByUserName(string username);
        User? GetUserByUserNameAndPassword(string username , string password);
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int user_id);
    }

    class ManageUserServices : IManageUserServices
    {
        private readonly NpgsqlConnection _context;
        private readonly IManageUserRoleService userRoleService;
        private readonly IManageRolesService rolesService;

        public ManageUserServices()
            :this(new ManageUserRoleService(),new ManageRolesService())
        { }

        public ManageUserServices(IManageUserRoleService userRoleService,
            IManageRolesService rolesService)
        {
            _context = (new ContextDB()).connect();
            _context.Close();
            this.userRoleService = userRoleService;
            this.rolesService = rolesService;
        }
        public bool AddUser(User user)
        {
            var user_temp = GetUserByUserName(user.username??"");
            if (user_temp == null)
            {
                _context.Close();
                _context.Open();
                string query = "insert into users (name,identity,date,username,password)" +
                    "values (@a,@b,@c,@d,@e)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("a", user.name ?? "");
                    cmd.Parameters.AddWithValue("b", user.identity);
                    var dateList = user.date.ToString().Split('/');
                    DateOnly date ;
                    if (dateList.Length == 3 && !dateList[2].Equals("0001"))
                        date = new DateOnly(int.Parse(dateList[2]), int.Parse(dateList[1]),int.Parse(dateList[0]));
                    else
                        date = new DateOnly(1800,01,01);
                    cmd.Parameters.AddWithValue("c", date);
                    cmd.Parameters.AddWithValue("d", user.username ?? "");
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(user.password!);
                    byte[] hashBytes;

                    using (SHA256 sHA256 = SHA256.Create())
                    {
                        hashBytes = sHA256.ComputeHash(passwordBytes);
                    }

                    string hashedPassword = Convert.ToBase64String(hashBytes);
                    cmd.Parameters.AddWithValue("e", hashedPassword);

                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }

        public bool DeleteUser(int user_id)
        {
            var user_temp = GetUserById(user_id);
            if (user_temp != null && user_temp.user_id != 1)
            {
                _context.Close();
                _context.Open();
                string query = "delete from users where user_id = @x ";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", user_id);

                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }

        public List<User>? GetAllUsers()
        {
            _context.Close();
            _context.Open();
            List<User>? users = null;
            string query = "select * from users order by user_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    users = new List<User>();
                    while (reader.Read())
                    {
                        var user = new User();
                        user.user_id = reader.GetInt32(0);
                        user.name = reader.GetString(1);
                        user.identity = reader.GetInt32(2);
                        var date = reader.GetDateTime(3);
                        user.date = new DateOnly(date.Year, date.Month, date.Day);
                        user.username = reader.GetString(4);
                        users.Add(user);

                    }
                    _context.Close();
                    return users;
                }
                _context.Close();
                return null;
            }
        }

        public User? GetUserById(int user_id)
        {
            _context.Close();
            _context.Open();
            User? user = null;
            string query = "select * from users where user_id = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x",user_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.user_id = reader.GetInt32(0);
                        user.name = reader.GetString(1);
                        user.identity = reader.GetInt32(2);
                        var date = reader.GetDateTime(3);
                        user.date = new DateOnly(date.Year, date.Month, date.Day);
                        user.username = reader.GetString(4);

                    }
                    _context.Close();
                    return user;
                }
                _context.Close();
                return null;
            }
        }

        public User? GetUserByUserName(string username)
        {
            _context.Close();
            _context.Open();
            User? user = null;
            string query = "select * from users where username = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", username??"");

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.user_id = reader.GetInt32(0);
                        user.name = reader.GetString(1);
                        user.identity = reader.GetInt32(2);
                        var date = reader.GetDateTime(3);
                        user.date = new DateOnly(date.Year,date.Month,date.Day);
                        user.username = reader.GetString(4);

                    }
                    _context.Close();
                    return user;
                }
                _context.Close();
                return null;
            }
        }



        public User? GetUserByUserNameAndPassword(string username, string password)
        {
            _context.Close();
            _context.Open();
            User? user = null;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes;

            using (SHA256 sHA256 = SHA256.Create())
            {
                hashBytes = sHA256.ComputeHash(passwordBytes);
            }

            string hashedPassword = Convert.ToBase64String(hashBytes);
            string query = "select * from users where username = @x and password = @y";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", username ?? "");
                cmd.Parameters.AddWithValue("y", hashedPassword ?? "");

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.user_id = reader.GetInt32(0);
                        user.name = reader.GetString(1);
                        user.identity = reader.GetInt32(2);
                        var date = reader.GetDateTime(3);
                        user.date = new DateOnly(date.Year, date.Month, date.Day);
                        user.username = reader.GetString(4);

                    }
                    _context.Close();
                    return user;
                }
                _context.Close();
                return null;
            }
        }

        public bool UpdateUser(User user)
        {
            var user_temp = GetUserById(user.user_id);
            if (user_temp != null)
            {
                _context.Close();
                _context.Open();
                string query = "update users set name = @a , identity = @b," +
                    "date = @c where user_id = @d";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("a", user.name ?? "");
                    cmd.Parameters.AddWithValue("b", user.identity);
                    var dateList = user.date.ToString().Split('/');
                    DateOnly date;
                    if (dateList.Length == 3 || !dateList[2].Equals("0001"))
                        date = new DateOnly(int.Parse(dateList[2]), int.Parse(dateList[1]), int.Parse(dateList[0]));
                    else
                        date = new DateOnly(1800, 01, 01);
                    cmd.Parameters.AddWithValue("c", date);
                    cmd.Parameters.AddWithValue("d", user.user_id);

                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }

        public List<User>? GetAllUsersByRole(string roleName)
        {
            var role = rolesService.GetRoleByName(roleName);
            List<User>? users = null;
            if(role != null)
            {
                var userRoles = userRoleService.GetAllUserRoleByRoleId(role.role_id);
                if(userRoles != null)
                {
                    users = new List<User>();
                    foreach (var item in userRoles)
                    {
                        var user = GetUserById(item.user_id);
                        if(user != null)
                            users.Add(user);
                    }
                }
            }
            return users;
        }
    }
}
