using Authn.Models;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class PartDAO
    {
        private readonly string connectionString;

        public PartDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Part GetPart(string Name)
        {
            var parts = new List<Part>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Type, Name, Description, Price, Quantity
                                        FROM Part
                                        WHERE Name = $Name";
                command.Parameters.AddWithValue("$Name", Name);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parts.Add(
                        new Part
                        {
                            Id = reader.GetInt32(0),
                            Type = reader.GetString(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3),
                            Price = reader.GetDecimal(4),
                            Quantity = reader.GetInt32(5)
                        });

                    }
                }
                if (parts.Count > 0)
                {
                    return parts[0];
                }
                else
                {
                    return null;
                }

            }
        }
        private bool isPartInDatabase(string Name, int quantity)
        {
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Type, Name, Description, Price, Quantity
                                        FROM Part
                                        WHERE Name = $Name AND Quantity>=$quantity";
                command.Parameters.AddWithValue("$Name", Name);
                command.Parameters.AddWithValue("$quantity", quantity);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public List<string> isEnoughPartInDatabase(Dictionary<string,int> partList)
        {
            var result = new List<string>();
            foreach (var item in partList)
            {
                using (var connection = new SqliteConnection(connectionString))
                {

                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT Id, Type, Name, Description, Price, Quantity
                                        FROM Part
                                        WHERE Name = $Name AND Quantity>=$quantity";
                    command.Parameters.AddWithValue("$Name", item.Key);
                    command.Parameters.AddWithValue("$quantity", item.Value);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                           result.Add(item.Key);
                        }
                    }
                }
            }
            return result;
            
        }
        public void DecrementPart(string Name, int number)
        {
            var parts = new List<Part>();
                using (var connection = new SqliteConnection(connectionString))
                {

                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Part SET
                                        Quantity = Quantity - $quantity      
                                        WHERE Name = $Name";
                    command.Parameters.AddWithValue("$Name", Name);
                    command.Parameters.AddWithValue("$quantity", number);
                    command.ExecuteNonQuery();
                }


        }
        public bool UpdateType(string oldName, string newName)
        {
            var repair = new List<Repair>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @" UPDATE Part
                                         SET Type = $newType
                                         WHERE Type = $oldType";
                command.Parameters.AddWithValue("$newType", newName);
                command.Parameters.AddWithValue("$oldType", oldName);
                command.ExecuteNonQuery();
                return true;

            }
        }
        public string GetPartName(int Id)
        {
            var parts = new List<Part>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Type, Name, Description, Price, Quantity
                                        FROM Part
                                        WHERE Id = $id";
                command.Parameters.AddWithValue("$id", Id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parts.Add(
                        new Part
                        {
                            Id = reader.GetInt32(0),
                            Type = reader.GetString(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3),
                            Price = reader.GetDecimal(4),
                            Quantity = reader.GetInt32(5)
                        });

                    }
                }
                if (parts.Count > 0)
                {
                    return parts[0].Name;
                }
                else
                {
                    return null;
                }

            }
        }
    }
}

