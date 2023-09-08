using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;

namespace MIS.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDb _dbContext;

        public UserController(ApplicationDb dbContext)
        {
            _dbContext = dbContext; // Dependency injection
        }
        public ActionResult Successful()
        {
            return View();
        }
        // GET: /User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [ValidateAntiForgeryToken]
        [HttpPost]
          public async Task<IActionResult> Login(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.Password))
            {
                // Set up claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username), // Add more claims if needed
                    // You can add additional claims such as roles here
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Set properties if needed
                };

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Redirect to a dashboard or home page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["a"] = "Username or Password Incorrect";
                TempData["meow"] = 10;
                return RedirectToAction("login");
            }
        }
       
        // GET: /User/SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SignUp(User newUser)
        {
            if (ModelState.IsValid)
            {
                newUser.Password = HashPassword(newUser.Password);

                // Check if the username is already taken
                if (_dbContext.Users.Any(u => u.Username == newUser.Username))
                {
                    TempData["var"] = "Username Already Taken";
                    TempData["result"] = 10;
                    return RedirectToAction("SignUp");
                }

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                // Registration successful, you can also log in the user here
                return RedirectToAction("Successful");
            }

            return View(newUser);
        }


        private string HashPassword(string password)
        {
            // Generate a salt for the hash
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            // Compare the entered password with the hashed password
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","User");
        }

    }
}
