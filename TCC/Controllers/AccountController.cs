using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TCC.Models;

namespace TCC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;

        public AccountController(UserManager<Usuario> user, SignInManager<Usuario> sign)
        {
            userManager = user;
            signInManager = sign;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, true, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Usuário Bloqueado");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "E-mail de Acesso e/ou Senhas Inválidos!");
                return View();
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
