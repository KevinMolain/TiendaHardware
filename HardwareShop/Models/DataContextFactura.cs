using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareShop.Models
{
    public class DataContextFactura
    {
        public string ConnectionString { get; set; }

        public DataContextFactura(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public void SetFactura(Factura nuevaFactura)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand(String.Format("INSERT INTO facturas(`FechaFactura`,`IdUsuario`) VALUES ('{0}','{1}');", nuevaFactura.FechaCompra.ToString("yyyy-MM-dd HH:mm:ss"), nuevaFactura.Account.Id), conn);       
                cmd1.ExecuteNonQuery();
                foreach (Item item in nuevaFactura.Items)
                {
                    MySqlCommand cmd2 = new MySqlCommand(String.Format("INSERT INTO lineafacturas(`Id_factura`,`Id_Producto`,`Cantididad`) VALUES ((SELECT max(id) FROM facturas),'{0}','{1}');", item.Product.Id, item.Quantity), conn);
                    cmd2.ExecuteNonQuery();
                }

            //    using (MySqlDataReader reader = cmd1.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {

            //        }
            //        conn.Close();
            //    }
            }
        }
    }
}
