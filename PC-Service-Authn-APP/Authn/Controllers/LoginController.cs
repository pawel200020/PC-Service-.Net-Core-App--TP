using Authn.Data;
using Authn.DataDAO;
using Authn.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authn.Controllers
{
    public class LoginController : Controller
    {
        [Authorize(Roles ="Admin")]
        public IActionResult List()
        {
            UserGetAllDB allUsers = new UserGetAllDB("DataSource=Data\\app.db");
            List<AppUser> users = allUsers.getAllUsers();
            return View(users);
        }
        public IActionResult Edit(int id)
        {
            var oneUserDB = new UserGetOneDB("DataSource=Data\\app.db", id);
            return View(oneUserDB.getUserVM());
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> ProcessEdit(AppUserVM user)
        {

            //UserAddDB usertoadd = new UserAddDB(user);
            if (true)
            {
                TempData["pass"] = "Account has been successfully modfied. You can sign in.";
                return View("list");
            }
            else
            {
                TempData["error"] = "some error occured please try again later";
                return View("list");
            }

        }


        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var oneUserDB = new UserGetOneDB("DataSource=Data\\app.db", id);
            return View(oneUserDB.getUser());
        }
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpGet("register")]
        public IActionResult Register(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidateLogin(string userName, string password, string returnUrl)
        {
            UserAuthDB claim = new UserAuthDB(userName, password);
            Dictionary<string, string> userInfo;
            List<string> roles;
            (userInfo, roles) = claim.ValidateUser();
            ViewData["returnUrl"] = returnUrl;
            if (userInfo.ContainsKey("UserName"))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("userName", userInfo["UserName"]));
                claims.Add(new Claim(ClaimTypes.Name, userInfo["FirstName"]));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userInfo["LastName"]));
                claims.Add(new Claim(ClaimTypes.Email, userInfo["Email"]));
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/Home/Index");
                }

            }
            TempData["error"] = "User name or passowrd is incorrect. Please try again";
            return View("login");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }
        [HttpPost("register")]
        public async Task<IActionResult> ProcessRegister(AppUserVM user)
        {

            UserAddDB usertoadd = new UserAddDB(user);
            if (usertoadd.AddUser())
            {
                TempData["register"] = "Account has been successfully created. You can sign in.";
                return View("login");
            }
            else
            {
                TempData["error"] = "User with provided userName or Email already exists, please try again";
                return View("Register");
            }
            
        }


    }
}
