using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareShop.Models
{
    public class DataContextProducts
    {
        private IConfigurationRoot configuration;

        public string ConnectionString { get; set; }

        public DataContextProducts(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public DataContextProducts(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Product", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            Id = reader.GetInt32("id"),
                            Usuario = reader.GetString("usuario"),
                            Contraseña = reader.GetString("contraseña")

                        });
                    }
                }
            }

            return list;
        }
    }
}
}
