using Authn.Models;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class RepairDAO
    {
        private readonly string connectionString;

        public RepairDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Repair GetRepair(int id)
        {
            var repair = new List<Repair>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Brand, Condition, Description, Email, ImageName, SerialNumber, Status, Report,Labour,PartsUsed
                                        FROM Repair
                                        WHERE Id= $id";
                command.Parameters.AddWithValue("$id", id);
                using (var reader = command.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        repair.Add(
                        new Repair
                        {
                            Id = reader.GetInt32(0),
                            Brand = reader.GetString(1),
                            Condition = reader.GetString(2),
                            Description = reader.GetString(3),
                            Email = reader.GetString(4),
                            ImageName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            SerialNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Status = reader.GetString(7),
                            Report = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Labour = reader.GetDecimal(9),
                            PartsUsed = reader.IsDBNull(10) ? null : reader.GetString(10),
                        });

                    }
                }
                if (repair.Count > 0)
                {
                    return repair[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public List<Repair> GetRepairsUser(string email)
        {
            var repair = new List<Repair>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Brand, Condition, Description, Email, ImageName, SerialNumber, Status, Report,Labour,PartsUsed, Date, Delivery
                                        FROM Repair
                                        WHERE Email = $email";
                command.Parameters.AddWithValue("$email", email);
                using (var reader = command.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        repair.Add(
                        new Repair
                        {
                            Id = reader.GetInt32(0),
                            Brand = reader.GetString(1),
                            Condition = reader.GetString(2),
                            Description = reader.GetString(3),
                            Email = reader.GetString(4),
                            ImageName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            SerialNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Status = reader.GetString(7),
                            Report = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Labour = reader.GetDecimal(9),
                            PartsUsed = reader.IsDBNull(10) ? null : reader.GetString(10),
                            Date = reader.GetDateTime(11),
                            Delivery = reader.GetString(12)
                        });

                    }
                }
                if (repair.Count > 0)
                {
                    return repair;
                }
                else
                {
                    return null;
                }
            }
        }


    }
}
