using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRN221_Assignment03_TranPhamKimSon_SE151317.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN221_Assignment03_TranPhamKimSon_SE151317.Controllers
{
    public class HomeController : Controller
    {

        SignalRAssignmentDB03Context dB03Context = new SignalRAssignmentDB03Context();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser) {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var tempUser = dB03Context.AppUsers.SingleOrDefault(user => user.Email == appUser.Email && user.Password == appUser.Password);
                if(tempUser != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", tempUser.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, tempUser.Password));
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimPrincipal);
                }
                else
                {
                    TempData["Error"] = "Login failed wrong email/password";
                    return View();
                }
                return Redirect("/Posts/Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
