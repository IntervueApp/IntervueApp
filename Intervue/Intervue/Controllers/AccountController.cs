using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Intervue.Data;
using Intervue.Models;
using Intervue.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Intervue.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel rvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            List<Claim> claims = new List<Claim>();

            ApplicationUser user = new ApplicationUser
            {
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                UserName = rvm.Email,
                Email = rvm.Email,
            };

            var result = await _userManager.CreateAsync(user, rvm.Password);

            if (result.Succeeded)
            {
                string fullName = $"{user.FirstName} {user.LastName}";
                Claim nameClaim = new Claim("FullName", fullName, ClaimValueTypes.String);
                Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimTypes.Email);


                claims.Add(nameClaim);
                claims.Add(emailClaim);

                await _userManager.AddClaimsAsync(user, claims);
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.Member);

                if (user.Email.Contains("@intervue.com") || user.Email.Contains("@codefellows.com"))
                {
                    await _userManager.AddToRoleAsync(user, ApplicationUserRoles.Admin);
                }

                await _context.SaveChangesAsync();
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}