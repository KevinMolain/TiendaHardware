using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareShop.Helpers;
using HardwareShop.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View("Checkout", model: factura);
        }

    }
}