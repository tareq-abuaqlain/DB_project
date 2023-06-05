using Npgsql;
using ProjectDB.Data;
using ProjectDB.Models;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace ProjectDB.Services
{
    public interface IManageRolesService
    {
        List<Role>? GetAllRolesByUser_id(int user_id);
        List<Role>? GetAllRoles();
        bool AddRole(Role role);
        bool RoleIsExit(int role_id);
        Role? GetRoleByName(string name);
        Role? GetRoleById(int role_id);
    }

    class ManageRolesService : IManageRolesService
    {

        private readonly NpgsqlConnection _context;

        public ManageRolesService()
        {
            _context = (new ContextDB()).connect();
            _context.Close();
        }


        public List<Role>? GetAllRolesByUser_id(int user_id)
        {
            _context.Close();
            _context.Open();
            List<int>? roles_id = null;
            List<Role>? roles = null;
            string query = "select * from user_role where user_id = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x",user_id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    roles_id = new List<int>();
                    while (reader.Read())
                    {
                        roles_id.Add(reader.GetInt32(1));
                    }
                }
                else
                {
                    _context.Close();
                    return null;
                }
                _context.Close();
            }
            
            roles = new List<Role>();
            foreach (var item in roles_id)
            {
                query = "select * from roles where role_id = @x";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    _context.Close();
                    _context.Open();
                    cmd.Parameters.AddWithValue("x", item);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null)
                    {

                        while (reader.Read())
                        {
                            var role = new Role();
                            role.role_id = reader.GetInt32(0);
                            role.name = reader.GetString(1);
                            roles.Add(role);
                        }
                    }
                    _context.Close();
                }

            }
            _context.Close();
            return roles;
        }

        public bool AddRole(Role role)
        {
            var role_temp = GetRoleByName(role.name??"");
            if (role_temp == null)
            {
                _context.Close();
                _context.Open();
                string query = "insert into roles (name) values (@x)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
                {
                    cmd.Parameters.AddWithValue("x", role.name??"");

                    int reader = cmd.ExecuteNonQuery();
                    _context.Close();
                    return (reader == -1) ? false : true;
                }
            }
            return false;
        }

        public Role? GetRoleByName(string name)
        {
            _context.Close();
            _context.Open();
            Role? role = null;
            string query = "select * from roles where name = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", name ?? "");

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        role = new Role();
                        role.role_id = reader.GetInt32(0);
                        role.name = reader.GetString(1);

                    }
                    _context.Close();
                    return role;
                }
                _context.Close();
                return null;
            }
        }

        public List<Role>? GetAllRoles()
        {
            _context.Close();
            _context.Open();
            List<Role>? roles = null;
            string query = "select * from roles";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    roles = new List<Role>();
                    while (reader.Read())
                    {
                        Role role = new Role();
                        role = new Role();
                        role.role_id = reader.GetInt32(0);
                        role.name = reader.GetString(1);
                        roles.Add(role);
                    }
                    _context.Close();
                    return roles;
                }
                _context.Close();
                return null;
            }
        }

        public bool RoleIsExit(int role_id)
        {
            var role = GetRoleById(role_id);
            if(role == null)
                return false;
            return true;
        }

        public Role? GetRoleById(int role_id)
        {
            _context.Close();
            _context.Open();
            Role? role = null;
            string query = "select * from roles where role_id = @x";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _context))
            {
                cmd.Parameters.AddWithValue("x", role_id);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        role = new Role();
                        role.role_id = reader.GetInt32(0);
                        role.name = reader.GetString(1);

                    }
                    _context.Close();
                    return role;
                }
                _context.Close();
                return null;
            }
        }
    }
}
