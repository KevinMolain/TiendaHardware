using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareShop.Models
{
    public class DataContextUsers
    {
        private IConfigurationRoot configuration;

        public string ConnectionString { get; set; }

        public DataContextUsers(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public DataContextUsers(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<Account> GetAllAccounts()
        {
            List<Account> list = new List<Account>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuarios", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Account()
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
        public void SetAccount(Account nuevaCuenta)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO usuarios VALUES ('{nuevaCuenta.Usuario}','{nuevaCuenta.Nombre}','{nuevaCuenta.Correo}','{nuevaCuenta.Contraseña}')");
            }
        }
    }
}
