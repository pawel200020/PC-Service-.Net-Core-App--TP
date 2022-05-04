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
                        if (reader.IsDBNull(5))
                        {
                            return null;
                        }
                            repair.Add(
                        new Repair
                        {
                            Id = reader.GetInt32(0),
                            ImageName = reader.GetString(5),
                        });

                    }
                }
                if(repair.Count > 0)
                {
                    return repair[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public Repair GetRepairPartsUsed(int id)
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
                        if (reader.IsDBNull(10))
                        {
                            return null;
                        }
                        repair.Add(
                    new Repair
                    {
                        Id = reader.GetInt32(0),
                        PartsUsed = reader.GetString(10),
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
    }
}
