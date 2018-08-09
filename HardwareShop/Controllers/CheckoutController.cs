using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HardwareShop.Helpers;
using HardwareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace HardwareShop.Controllers
{
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        [Route("checkout")]
        public IActionResult Checkout(int id, string usuario)
        {
            if (id == 0)
            {
                return View("../Account/Login");
            }
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Factura factura = new Factura();
            factura.Items = cart;
            DateTime FechaActual =
            factura.FechaCompra = DateTime.Now;
            DataContextUsers db = HttpContext.RequestServices.GetService(typeof(DataContextUsers)) as DataContextUsers;
            List<Account> listaUsuarios = db.GetAllAccounts();
            foreach (Account a in listaUsuarios)
            {
                if (a.Usuario == usuario)
                {
                    factura.Account = a;
                }
            }
            //DataContextFactura dbf = HttpContext.RequestServices.GetService(typeof(DataContextFactura)) as DataContextFactura;
            DataContextFactura dbf = new DataContextFactura("server=127.0.0.1;port=3306;database=pk;user=admin;password=1111");
            dbf.SetFactura(factura);
            return View("Checkout", model: factura);
        }


    }
}