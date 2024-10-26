using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UserManagementConsoleApp
{
    public class Program
    {
        public static List<User> GetUsers()
        {
            var users = new List<User>();
            string connectionString = "Server=NS\\SQLEXPRESS01;Database=TestDB;Trusted_Connection=True;";
            // connection string

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Username, Email, CreationDate FROM Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                                CreationDate = Convert.ToDateTime(reader["CreationDate"])
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        public static void Main(string[] args)
        {
            var users = GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"Username: {user.Username}, Email: {user.Email}, Created On: {user.CreationDate}");
            }
            Console.ReadKey();
        }
    }
}
