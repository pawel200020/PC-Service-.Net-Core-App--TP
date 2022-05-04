//using Authn.Models;
//using Microsoft.Data.Sqlite;

//namespace Authn.DataDAO
//{
//    public class PartGetOne
//    {
//        private readonly string connectionString;

//        public PartGetOne(string connectionString)
//        {
//            this.connectionString = connectionString;
//        }
//        public Part? GetPart(string Name)
//        {
//            var parts = new List<Part>();
//            using (var connection = new SqliteConnection(connectionString))
//            {

//                connection.Open();

//                var command = connection.CreateCommand();
//                command.CommandText = @"SELECT Id, Type, Name, Description, Price, Quantity
//                                        FROM Part
//                                        WHERE Name = $Name";
//                command.Parameters.AddWithValue("$Name", Name);
//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        parts.Add(
//                        new Part
//                        {
//                            Id = reader.GetInt32(0),
//                            Type = reader.GetString(1),
//                            Name = reader.GetString(2),
//                            Description = reader.GetString(3),
//                            Price = reader.GetDecimal(4),
//                            Quantity = reader.GetInt32(5)
//                        });

//                    }
//                }
//                if (parts.Count > 0)
//                {
//                    return parts[0];
//                }
//                else
//                {
//                    return null;
//                }
                
//            }
//        }
//    }
//}
