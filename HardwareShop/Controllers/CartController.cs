using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareShop.Helpers;
using HardwareShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace HardwareShop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            //ViewBag.cart = cart;
            //ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View(cart);
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("removeall")]
        public IActionResult RemoveAll()
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            foreach (Item i in cart)
            {
                cart.Remove(i);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("checkout")]
        public IActionResult Checkout(int id)
        {
            if (id == 0)
            {
                return View("../Account/Login");
            }
            return View("Checkout");
        }


        //METODO AUXILIARRRRRRRR
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}