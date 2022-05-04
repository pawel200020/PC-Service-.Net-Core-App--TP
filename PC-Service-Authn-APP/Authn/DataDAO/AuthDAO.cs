using Authn.Data;
using Authn.Models;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class AuthDAO
    {
        private readonly string connectionString;

        public AuthDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #region Validate User

        #endregion
        public Tuple<Dictionary<string, string>, List<string>> ValidateUser(string userName, string password)
        {
            var userPrincipals = new Dictionary<string, string>();
            var userRoles = new List<string>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT UserName, Password, Email, FirstName, LastName, Roles
                                        FROM AppUsers 
                                        Where UserName = $userName AND Password = $password";
                command.Parameters.AddWithValue("$userName", userName);
                command.Parameters.AddWithValue("$password", password);
                /*
                0 - UserName
                1 - Password
                2 - Email
                3 - FirstName
                4 - LastName
                5 - Roles
                 */
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userPrincipals.Add("UserName", reader.GetString(0));
                        //userPrincipals.Add("Password", reader.GetString(1));
                        userPrincipals.Add("Email", reader.GetString(2));
                        userPrincipals.Add("FirstName", reader.GetString(3));
                        userPrincipals.Add("LastName", reader.GetString(4));
                        userRoles = reader.GetString(5).Split(',').ToList();

                    }
                }
            }
            return new Tuple<Dictionary<string, string>, List<string>>(userPrincipals, userRoles);
        }
        private bool isUserInBase(AppUserVM user)
        {
            int RowCount = 0;
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT count(UserId)
                                        FROM AppUsers 
                                        Where UserName = $userName OR Email = $email";
                command.Parameters.AddWithValue("$userName", user.UserName);
                command.Parameters.AddWithValue("$email", user.Email);
                RowCount = Convert.ToInt32(command.ExecuteScalar());
            }
            if (RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region AddUser 
        #endregion
        public bool AddUser(AppUserVM user)
        {
            if (isUserInBase(user))
            {
                return false;
            }
            else
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO AppUsers (UserName, Provider, NameIdentifier, Password, Email, FirstName, LastName, Mobile, Address, City, Country, Roles)
                                            VALUES ($UserName, $Provider, $NameIdentifier, $Password, $Email, $FirstName, $LastName, $Mobile, $Address, $City, $Country, $Roles)
                                            ";
                    command.Parameters.AddWithValue("$UserName", user.UserName);
                    command.Parameters.AddWithValue("$Provider", "Cookies");
                    command.Parameters.AddWithValue("$NameIdentifier", user.FirstName);
                    command.Parameters.AddWithValue("$Password", user.Password);
                    command.Parameters.AddWithValue("$Email", user.Email);
                    command.Parameters.AddWithValue("$FirstName", user.FirstName);
                    command.Parameters.AddWithValue("$LastName", user.LastName);
                    command.Parameters.AddWithValue("$Mobile", user.Mobile);
                    command.Parameters.AddWithValue("$Address", user.Address);
                    command.Parameters.AddWithValue("$City", user.City);
                    command.Parameters.AddWithValue("$Country", user.Country);
                    if (user.Roles.Length > 0)
                    {
                        command.Parameters.AddWithValue("$Roles", user.Roles);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("$Roles", "");
                    }

                    command.ExecuteNonQuery();
                }
                return true;
            }

        }
        public bool EditUser(AppUserVM user)
        {
            if (!isUserInBase(user))
            {
                return false;
            }
            else
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"UPDATE AppUsers SET
                                            UserName = $UserName, Email = $Email, FirstName = $FirstName, LastName = $LastName, Mobile = $Mobile, Address = $Address, City = $City, Country = $Country, Roles = $Roles
                                            WHERE UserId = $UserId
                                            ";
                    command.Parameters.AddWithValue("$UserId", user.UserId);
                    command.Parameters.AddWithValue("$UserName", user.UserName);
                    command.Parameters.AddWithValue("$Provider", "Cookies");
                    command.Parameters.AddWithValue("$NameIdentifier", user.FirstName);
                    command.Parameters.AddWithValue("$Email", user.Email);
                    command.Parameters.AddWithValue("$FirstName", user.FirstName);
                    command.Parameters.AddWithValue("$LastName", user.LastName);
                    command.Parameters.AddWithValue("$Mobile", user.Mobile);
                    command.Parameters.AddWithValue("$Address", user.Address);
                    command.Parameters.AddWithValue("$City", user.City);
                    command.Parameters.AddWithValue("$Country", user.Country);
                    if (user.Roles.Length > 0)
                    {
                        command.Parameters.AddWithValue("$Roles", user.Roles);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("$Roles", "");
                    }

                    command.ExecuteNonQuery();
                }
                return true;
            }

        }
        public bool ChangeUserPassword(AppUserVM user)
        {
            
            if (!isUserInBase(user))
            {
                return false;
            }
            else
            {
                using (var connection = new SqliteConnection("DataSource=Data\\app.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"UPDATE AppUsers SET
                                            Password = $Password
                                            WHERE UserName = $UserName
                                            ";
                    command.Parameters.AddWithValue("$Password", user.Password);
                    command.Parameters.AddWithValue("$UserName", user.UserName);
                    command.ExecuteNonQuery();
                }
                return true;
            }

        }
        public bool DeleteUser(AppUserVM user)
        {
            if (!isUserInBase(user))
            {
                return false;
            }
            else
            {
                using (var connection = new SqliteConnection("DataSource=Data\\app.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"DELETE FROM AppUsers
                                            WHERE UserName = $UserName";
                    command.Parameters.AddWithValue("$UserName", user.UserName);
                    command.ExecuteNonQuery();
                }
                return true;
            }

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
            }
            return users;
        }
        public AppUser getUser(int id)
        {
            AppUser user = new AppUser();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT UserId, UserName, Provider, NameIdentifier, 
                                        Password, Email, FirstName, LastName, Mobile, 
                                        Address, City, Country, Roles
                                        FROM AppUsers
                                        Where UserId = $id";
                command.Parameters.AddWithValue("$id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user = new AppUser
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
                        };

                    }
                }

                return user;
            }
        }
        public AppUserVM getUserVM(int id)
        {
            AppUserVM user = new AppUserVM();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT UserId, UserName, 
                                        Password, Email, FirstName, LastName, Mobile, 
                                        Address, City, Country, Roles
                                        FROM AppUsers
                                        Where UserId = $id";
                command.Parameters.AddWithValue("$id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user = new AppUserVM
                        {
                            UserId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            Email = reader.GetString(3),
                            FirstName = reader.GetString(4),
                            LastName = reader.GetString(5),
                            Mobile = reader.GetString(6),
                            Address = reader.GetString(7),
                            City = reader.GetString(8),
                            Country = reader.GetString(9),
                            Roles = reader.GetString(10)
                        };

                    }
                }
            }
            return user;
        }
    }
}
