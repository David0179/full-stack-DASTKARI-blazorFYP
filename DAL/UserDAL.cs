using Entities;
using Microsoft.Data.SqlClient;
using System;

namespace DAL
{
    public class UserDAL
    {
        public AppUser GetUserByEmail(string email)
        {
            using SqlConnection conn = DBHelper.GetConnection();
            string query = "SELECT * FROM Users WHERE Email = @Email";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AppUser
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    Role = reader["Role"].ToString()
                };
            }

            return null;
        }

        public int GetTotalUserCount()
        {
            using SqlConnection conn = DBHelper.GetConnection();
            string query = "SELECT COUNT(*) FROM Users";
            using SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            return (int)cmd.ExecuteScalar();
        }

        public void AddUser(AppUser user)
        {
            using SqlConnection conn = DBHelper.GetConnection();
            string query = "INSERT INTO Users (Name, Email, Role) VALUES (@Name, @Email, @Role)";
            using SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Role", user.Role);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
