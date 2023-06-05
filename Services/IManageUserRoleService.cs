using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;

namespace ProjectDB.Services
{
    public interface IManageUserRoleService
    {
        UserRole? GetUserRole(int user_id, int role_id);
        List<UserRole>? GetAllUserRole(int user_id);
        List<UserRole>? GetAllUserRole();
        List<UserRole>? GetAllUserRoleByRoleId(int role_id);
        bool DeleteAllUserRole(int user_id);
        bool AddUserRole(int user_id , int role_id);
    }

    class ManageUserRoleService : IManageUserRoleService
    {
        private readonly NpgsqlConnection _context;

        public ManageUserRoleService()
        {
            _context = (new ContextDB()).connect();
            _context.Close();
        }

        public bool AddUserRole(int user_id, int role_id)
        {
            var userRoleTemp = GetUserRole(user_id, role_id);
            if (userRoleTemp == null)
            {
                _context.Close();
                _context.Open();
                UserRole? userRole = new UserRole();
                userRole.user_id = user_id;
                userRole.role_id = role_id;
                string query = "insert into user_role values (@x,@y)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", user_id);
                    cmd.Parameters.AddWithValue("y", role_id);

                    int reader = cmd.ExecuteNonQuery();
                    return (reader == -1)?false:true;
                }
            }
            _context.Close();
            return false;
        }

        public UserRole? GetUserRole(int user_id, int role_id)
        {
            _context.Close();
            _context.Open();
            UserRole? userRole = null;
            string query = "select * from user_role where user_id =@x and role_id = @y";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", user_id);
                cmd.Parameters.AddWithValue("y", role_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        userRole = new UserRole();
                        userRole.user_id = reader.GetInt32(0);
                        userRole.role_id = reader.GetInt32(1);

                    }
                    _context.Close();
                    return userRole;
                }
                _context.Close();
                return null;
            }
        }

        public List<UserRole>? GetAllUserRole(int user_id)
        {
            _context.Close();
            _context.Open();
            List<UserRole>? userRoles = null;
            string query = "select * from user_role where user_id =@x ";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", user_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    userRoles = new List<UserRole>();
                    while (reader.Read())
                    {
                        var userRole = new UserRole();
                        userRole = new UserRole();
                        userRole.user_id = reader.GetInt32(0);
                        userRole.role_id = reader.GetInt32(1);
                        userRoles.Add(userRole);
                    }
                    _context.Close();
                    return userRoles;
                }
                _context.Close();
                return null;
            }

        }

        public bool DeleteAllUserRole(int user_id)
        {
            _context.Close();
            _context.Open();
            string query = "delete from user_role where user_id = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", user_id);

                int reader = cmd.ExecuteNonQuery();
                _context.Close();
                return (reader == -1)?false:true;
            }
        }

        public List<UserRole>? GetAllUserRoleByRoleId(int role_id)
        {
            _context.Close();
            _context.Open();
            List<UserRole>? userRoles = null;
            string query = "select * from user_role where role_id =@x ";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", role_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    userRoles = new List<UserRole>();
                    while (reader.Read())
                    {
                        var userRole = new UserRole();
                        userRole = new UserRole();
                        userRole.user_id = reader.GetInt32(0);
                        userRole.role_id = reader.GetInt32(1);
                        userRoles.Add(userRole);
                    }
                    _context.Close();
                    return userRoles;
                }
                _context.Close();
                return null;
            }
        }

        public List<UserRole>? GetAllUserRole()
        {
            _context.Close();
            _context.Open();
            List<UserRole>? userRoles = null;
            string query = "select * from user_role  ";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    userRoles = new List<UserRole>();
                    while (reader.Read())
                    {
                        var userRole = new UserRole();
                        userRole = new UserRole();
                        userRole.user_id = reader.GetInt32(0);
                        userRole.role_id = reader.GetInt32(1);
                        userRoles.Add(userRole);
                    }
                    _context.Close();
                    return userRoles;
                }
                _context.Close();
                return null;
            }
        }
    }
}
