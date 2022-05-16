using Authn.Models;
using Microsoft.Data.Sqlite;

namespace Authn.DataDAO
{
    public class DelivertiesDAO
    {
        private readonly string connectionString;

        public DelivertiesDAO(string connectionString)
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
        public decimal GetDeliveryPrice(string Name)
        {
            var deliveries = new List<Delivery>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Name, Price
                                        FROM Delivery
                                        WHERE Name = $name";
                command.Parameters.AddWithValue("$name", Name);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deliveries.Add(
                        new Delivery
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        });

                    }
                }
                if(deliveries.Count > 0)
                {
                    return deliveries[0].Price;
                }
                else
                {
                    return 0;
                }
            }

        }
        public string GetDeliveryName(int Id)
        {
            var deliveries = new List<Delivery>();
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, Name, Price
                                        FROM Delivery
                                        WHERE Id = id";
                command.Parameters.AddWithValue("$id", Id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deliveries.Add(
                        new Delivery
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        });

                    }
                }
                if (deliveries.Count > 0)
                {
                    return deliveries[0].Name;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
