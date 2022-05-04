//using Microsoft.Data.Sqlite;

//namespace Authn.DataDAO
//{
//    public class UserAuthDB
//    {
//        private string userName;
//        private string password;
//        public UserAuthDB(string userName, string password)
//        {
//            this.userName = userName;
//            this.password = password;
//        }
//        public Tuple<Dictionary<string,string>,List<string>> ValidateUser()
//        {
//            var userPrincipals = new Dictionary<string,string>();
//            var userRoles = new List<string>();
//            using (var connection = new SqliteConnection("DataSource=Data\\app.db"))
//            {
//                connection.Open();

//                var command = connection.CreateCommand();
//                command.CommandText = @"SELECT UserName, Password, Email, FirstName, LastName, Roles
//                                        FROM AppUsers 
//                                        Where UserName = $userName AND Password = $password";
//                command.Parameters.AddWithValue("$userName", userName);
//                command.Parameters.AddWithValue("$password", password);
//                /*
//                0 - UserName
//                1 - Password
//                2 - Email
//                3 - FirstName
//                4 - LastName
//                5 - Roles
//                 */
//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        userPrincipals.Add("UserName", reader.GetString(0));
//                        //userPrincipals.Add("Password", reader.GetString(1));
//                        userPrincipals.Add("Email", reader.GetString(2));
//                        userPrincipals.Add("FirstName", reader.GetString(3));
//                        userPrincipals.Add("LastName", reader.GetString(4));
//                        userRoles = reader.GetString(5).Split(',').ToList();

//                    }
//                }
//            }
//            return new Tuple<Dictionary<string, string>, List<string>>(userPrincipals,userRoles);
//        }
//    }
//}
