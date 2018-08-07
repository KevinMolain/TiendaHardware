using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        [Route("Login")]
        public IActionResult Index()
        {
            return View("Login");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View("register");
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

        public IActionResult Verify()
        {
            return View("Index");
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

        [Route("remember")]
        [HttpPost]
        public IActionResult Remember(string usuario)
        {
            // Leer de base de datos
            DataContextUsers db = HttpContext.RequestServices.GetService(typeof(DataContextUsers)) as DataContextUsers;
            List<Account> listaUsuarios = db.GetAllAccounts();

            // servidor SMTP

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("hiberusclaseaspcoremvc@gmail.com", "111??aaa");
            client.EnableSsl = true;

            // 
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("hiberusclaseaspcoremvc@gmail.com");
            foreach(Account a in listaUsuarios)
            {
                if (a.Usuario == usuario)
                {
                    mailMessage.To.Add(a.Correo);
                    mailMessage.Body = "La contraseña que tenías era" + a.Contraseña + "a ver si no la vuelves a olvidar melón";
                }               
            }            
            mailMessage.Subject = "Olvidación de la contraseña";

            string output = "enviado";
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception e) { output = e.ToString() + "no enviado"; }

            ViewBag.message = output;
            return View("Success");
        }
    }
}