using Authn.Models;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class DelivertiesGetToList
    {
        private readonly string connectionString;

        public DelivertiesGetToList(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Delivery> GetDeliveryMethods()
        {
            var deliveryMethods = new List<Delivery>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Name, Price
                                        FROM Delivery";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deliveryMethods.Add(
                        new Delivery
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        });

                    }
                }

                return deliveryMethods;
            }
        }
    }
}
