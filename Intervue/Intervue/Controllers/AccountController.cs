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

        /// <summary>
        /// This is the Register (Account View folder, Register page). Notice the retrieval, HttpGet.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// This is the functionality of posting the new registration into the database. The claims are the first name, last name and email. The email will also be acting as the username.
        /// </summary>
        /// <param name="rvm"></param>
        /// <returns></returns>
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

            //Each user requires a password paired with their username.
            var result = await _userManager.CreateAsync(user, rvm.Password);

            //if the password and username pairing succeeds, their information will go through claims to see if the requirements have passed on. In this case, the name and email claim will be checked. If the email contains @intervue or @codefellows, they will become admin. Otherwise, everyone else who registers and passes claims will become a member. Once this is saved into the database, the user will be redirected back to the index view of the Home folder.
            if (result.Succeeded)
            {
                //
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

        /// <summary>
        /// This is a retrieval for the Login (Account view folder, Login page)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// This is the login method. If the state is valid, the signInManager will check with the PasswordSignInAsync. And this will check the email, password, if it is not persistent and not locked out. If this succeeds, this redirects into the Index view in the Home Folder..
        /// </summary>
        /// <param name="lvm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        /// <summary>
        /// This is the logout method. If the state is valid, the signInManager will call SignOutAsync and sign the user out. If so, the logout will be successful and redirect back to the Index view of the Home folder.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
                ViewData["LoggedOut"] = "Logout successful";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}