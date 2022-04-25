using Authn.Data;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class UserGetAllDB
    {
        private string connectionString;
        public UserGetAllDB(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<AppUser> getAllUsers()
        {
            List<AppUser> users = new List<AppUser>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT UserId, UserName, Provider, NameIdentifier, 
                                        Password, Email, FirstName, LastName, Mobile, 
                                        Address, City, Country, Roles
                                        FROM AppUsers";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(
                        new AppUser
                        {
                            UserId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Provider = reader.GetString(2),
                            NameIdentifier = reader.GetString(3),
                            Password = reader.GetString(4),
                            Email = reader.GetString(5),
                            FirstName = reader.GetString(6),
                            LastName = reader.GetString(7),
                            Mobile = reader.GetString(8),
                            Address = reader.GetString(9),
                            City = reader.GetString(10),
                            Country = reader.GetString(11),
                            Roles = reader.GetString(12)
                        });

                    }
                }

                return users;
            }
        }
    }
}
