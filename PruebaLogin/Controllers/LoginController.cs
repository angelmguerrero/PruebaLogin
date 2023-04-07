using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PruebaLogin.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;




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
        public IActionResult UserLogin([Bind] Users user)
        {
            var users = new Users();
            var allUsers = users.GetUser().FirstOrDefault();

            if (users.GetUser().Any(u => u.UserName == user.UserName))
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, "anet@test.com"),
                    new Claim(ClaimTypes.Role, "Administrador"),
                    new Claim(ClaimTypes.Role, "Analista"),
                    new Claim(ClaimTypes.Role, "Supervisor"),

                };

                var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Users", "Home");
            }

            return View(user);
        }
    }
}