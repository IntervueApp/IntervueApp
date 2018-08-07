using IntervueApp.Models;
using IntervueApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IntervueApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Do we need this??
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel lvm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

        //        if (result.Succeeded)
        //        {
        //            //{   //send an email welcoming the user back
        //            //    var user = await _userManager.FindByEmailAsync(lvm.Email);
        //            //    await _emailSender.SendEmailAsync(user.Email, "You've logged in",
        //            //        "<h1><font color='blue'>You must really like our store!</font><h1>" +
        //            //        "<h2>Our featured product this week is the <font color='red'>Mars Rover.</font></h2>" +
        //            //        "<p>Buy one <b>today</b> and you'll be prepared to travel around your new planet in <i>style</i>.</p>");
        //            //if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
        //            //{
        //            //    return RedirectToAction("Index", "Admin");
        //            //}

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "You don't know your credentials.");
        //        }
        //    }
        //    return View(lvm);
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                TempData["ErrorMessage"] = "Error from provider";
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return RedirectToAction("About", "Home");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            string lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);

            return View("ExternalLogin", new ExternalLoginViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel elvm)
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)

                {
                    TempData["Error"] = "Error loading information";
                }

                //RegisterViewModel rvm = new RegisterViewModel
                //{
                //    FirstName = elvm.FirstName,
                //    LastName = elvm.LastName,
                //    Email = elvm.Email
                //};

                var user = new ApplicationUser
                {
                    FirstName = elvm.FirstName,
                    LastName = elvm.LastName,
                    UserName = elvm.Email,
                    Email = elvm.Email
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("About", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["LoggedOut"] = "User Logged Out";
            return RedirectToAction("Index", "Home");
        }
    }
}