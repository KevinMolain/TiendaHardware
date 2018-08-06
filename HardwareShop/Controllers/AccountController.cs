using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardwareShop.Helpers;
using HardwareShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HardwareShop.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string usuario, string contrasena)
        {
            DataContextUsers db = HttpContext.RequestServices.GetService(typeof(DataContextUsers)) as DataContextUsers;
            List<Account> listaUsuarios = db.GetAllAccounts();

            foreach(Account a in listaUsuarios)
            {
                if ((usuario == a.Usuario)&&(contrasena == a.Contraseña))
                {
                    HttpContext.Session.SetString("usuario", usuario);
                    return View("Success");
                    //HE PUESTO ESTA VISTA POR DEFECTO, PERO YA LA CAMBIAREMOS
                }
            }
            ViewBag.error = "Usuario o contraseña incorrectos";
            return View("Index");           
        }


        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

        [Route("add")]
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(Account nuevaCuenta)
        {
            DataContextUsers db = HttpContext.RequestServices.GetService(typeof(DataContextUsers)) as DataContextUsers;
            List<Account> listaUsuarios = db.GetAllAccounts();
            foreach(Account a in listaUsuarios)
            {
                if ((nuevaCuenta.Usuario == a.Usuario))
                {
                    ViewBag.error = "Ya existe ese nombre de usuario";
                    return View("Add");
                }
            }
            db.SetAccount(nuevaCuenta);
            return RedirectToAction("index");
            //LO MISMO, ESTA VISTA HABRÁ QUE MODIFICARLA
        }
    }
}