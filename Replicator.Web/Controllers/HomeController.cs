using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Replicator.Web.Models;

namespace Replicator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["FirstName"] = "";
            ViewData["LastName"] = "";
            ViewData["Email"] = "";
            ViewData["Password"] = "";
            ViewData["RePassword"] = "";
            ViewData["FirstNameColor"] = "#000000";
            ViewData["LastNameColor"] = "#000000";
            ViewData["EmailColor"] = "#000000";
            ViewData["PasswordColor"] = "#000000";
            ViewData["RePasswordColor"] = "#000000";
            return View();
        }

        [HttpPost]
        public IActionResult Register(string FirstName, string LastName, string Email, string Password, string RePassword)
        {
            bool validFirstName = (!(FirstName is null) && FirstName != "");
            bool validLastName = (!(LastName is null) && LastName != "");
            bool validEmail = (!(Email is null) && Email != "" && Regex.IsMatch(Email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"));
            bool validPassword = (!(Password is null) && Password != "" && Regex.IsMatch(Password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"));

            if (validFirstName && validLastName && validEmail && validPassword && Password == RePassword)
            {
                return RedirectToAction("RegistrationComplete", "Home", new { firstName = FirstName, lastName = LastName, email = Email});
            }
            ViewData["FirstName"] = FirstName;
            ViewData["LastName"] = LastName;
            ViewData["Email"] = Email;
            ViewData["Password"] = Password;
            ViewData["RePassword"] = RePassword;
            ViewData["FirstNameColor"] = validFirstName ? "#20FF20" : "#FF2020";
            ViewData["LastNameColor"] = validLastName ? "#20FF20" : "#FF2020";
            ViewData["EmailColor"] = validEmail ? "#20FF20" : "#FF2020";
            ViewData["PasswordColor"] = validPassword ? "#20FF20" : "#FF2020";
            ViewData["RePasswordColor"] = (!(RePassword is null) && RePassword == Password) ? "#20FF20" : "#FF2020";
            return View();
        }

        public IActionResult RegistrationComplete(string firstName, string lastName, string email)
        {
            ViewData["FirstName"] = firstName;
            ViewData["LastName"] = lastName;
            ViewData["Email"] = email;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
