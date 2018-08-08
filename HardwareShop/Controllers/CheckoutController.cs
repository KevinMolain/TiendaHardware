using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace HardwareShop.Controllers
{
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        [Route("checkout")]
        public IActionResult Checkout(int id)
        {
            if (id == 0)
            {
                return View("../Account/Login");
            }
            return View("Checkout");
        }

    }
}