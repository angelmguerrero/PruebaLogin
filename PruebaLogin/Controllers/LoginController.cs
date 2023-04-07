using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PruebaLogin.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;




namespace PruebaLogin.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }


        [HttpPost]
        public IActionResult UserLogin([Bind] Usuario user)
        {


            var context = new CPMLoginContext();
            var studentsWithSameName = context.Usuario
                                              .Where(s => s.Email == user.Email && s.Password == user.Password)
                                              .FirstOrDefault();

            if (studentsWithSameName != null)
            {
                var lstOperacion = (from u in context.Usuario
                                    join r in context.Rol on u.IdRol equals r.Id
                                    join o in context.RolOperacion on r.Id equals o.IdRol
                                    join op in context.Operaciones on o.IdOperacion equals op.Id
                                    select op
                                    ).ToList();

                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),

                };

                foreach (Operaciones item in lstOperacion)
                {
                   userClaims.Add( new Claim(ClaimTypes.Role, item.Nombre));
                }

                var grandmaIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Users", "Home");
            }

            ViewBag.Error = "Usuario o contraseña invalida";
            return View();
        }

        public IActionResult Salir()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");

        }

    }
}