using Authn.Models;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class UserAddDB
    {
        private AppUserVM user;
        public UserAddDB(AppUserVM user)
        {
            this.user = user;
        }
        public bool AddUser()
        {
            int RowCount = 0;
            using (var connection = new SqliteConnection("DataSource=Data\\app.db"))
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
            if(RowCount > 0)
            {
                return false;
            }
            else
            {
                using (var connection = new SqliteConnection("DataSource=Data\\app.db"))
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
    }
}
/*
 * INSERT INTO table_name (column1, column2, column3, ...)
VALUES (value1, value2, value3, ...)
 */