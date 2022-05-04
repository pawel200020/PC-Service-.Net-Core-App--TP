using Authn.Models;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class PartTypesDAO
    {
        private readonly string connectionString;

        public PartTypesDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Delivery> GetPartTypes()
        {
            var deliveryMethods = new List<Delivery>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Name
                                        FROM PartsTypes";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deliveryMethods.Add(
                        new Delivery
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });

                    }
                }

                return deliveryMethods;
            }
        }
    }
}
