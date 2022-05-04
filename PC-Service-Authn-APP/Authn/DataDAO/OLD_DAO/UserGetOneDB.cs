//using Authn.Data;
//using Authn.Models;
//using Microsoft.Data.Sqlite;

//namespace Authn.DataDAO
//{
//    public class UserGetOneDB
//    {
//        private string connectionString;
//        private int id;
//        public UserGetOneDB(string connectionString, int id)
//        {
//            this.connectionString = connectionString;
//            this.id = id;
//        }
//        public AppUser getUser()
//        {
//            AppUser user = new AppUser();
//            using (var connection = new SqliteConnection(connectionString))
//            {
//                connection.Open();

//                var command = connection.CreateCommand();
//                command.CommandText = @"SELECT UserId, UserName, Provider, NameIdentifier, 
//                                        Password, Email, FirstName, LastName, Mobile, 
//                                        Address, City, Country, Roles
//                                        FROM AppUsers
//                                        Where UserId = $id";
//                command.Parameters.AddWithValue("$id", id);
//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {

//                        user = new AppUser
//                        {
//                            UserId = reader.GetInt32(0),
//                            UserName = reader.GetString(1),
//                            Provider = reader.GetString(2),
//                            NameIdentifier = reader.GetString(3),
//                            Password = reader.GetString(4),
//                            Email = reader.GetString(5),
//                            FirstName = reader.GetString(6),
//                            LastName = reader.GetString(7),
//                            Mobile = reader.GetString(8),
//                            Address = reader.GetString(9),
//                            City = reader.GetString(10),
//                            Country = reader.GetString(11),
//                            Roles = reader.GetString(12)
//                        };

//                    }
//                }

//                return user;
//            }
//        }
//        //public AppUserVM getUserVM()
//        //{
//        //    AppUserVM user = new AppUserVM();
//        //    using (var connection = new SqliteConnection(connectionString))
//        //    {
//        //        connection.Open();

//        //        var command = connection.CreateCommand();
//        //        command.CommandText = @"SELECT UserId, UserName, 
//        //                                Password, Email, FirstName, LastName, Mobile, 
//        //                                Address, City, Country, Roles
//        //                                FROM AppUsers
//        //                                Where UserId = $id";
//        //        command.Parameters.AddWithValue("$id", id);
//        //        using (var reader = command.ExecuteReader())
//        //        {
//        //            while (reader.Read())
//        //            {

//        //                user = new AppUserVM
//        //                {
//        //                    UserId = reader.GetInt32(0),
//        //                    UserName = reader.GetString(1),
//        //                    Password = reader.GetString(2),
//        //                    Email = reader.GetString(3),
//        //                    FirstName = reader.GetString(4),
//        //                    LastName = reader.GetString(5),
//        //                    Mobile = reader.GetString(6),
//        //                    Address = reader.GetString(7),
//        //                    City = reader.GetString(8),
//        //                    Country = reader.GetString(9),
//        //                    Roles = reader.GetString(10)
//        //                };

//        //            }
//        //        }

//        //        return user;
//        //    }
//        //}
//    }
//}
