using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace HardwareShop.Controllers
{
    [Route("portatil")]
    public class PortatilController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            DataContextProducts db = HttpContext.RequestServices.GetService(typeof(DataContextProducts)) as DataContextProducts;
            List<Product> listaProductos = db.GetAllProducts();

            foreach (Product p in listaProductos)
            {
                if (p.Tipo.Contains("portatil"))
                {
                    return View(model: p);
                }
            }
            return View();
        }
    }
}