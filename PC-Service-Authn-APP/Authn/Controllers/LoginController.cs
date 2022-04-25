using Authn.Data;
using Authn.DataDAO;
using Authn.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Authn.Controllers
{
    public class LoginController : Controller
    {

        private readonly IConfiguration _configuration;
        

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        //List<AppUser> users = (new UserGetAllDB(_configuration.GetConnectionString("DefaultConnection"))).getAllUsers();






        //List Main Element
        [Authorize(Roles ="Admin")]
        public IActionResult List()
        {
            UserGetAllDB allUsers = new UserGetAllDB("DataSource=Data\\app.db");
            List<AppUser> users = allUsers.getAllUsers();
            return View(users);
        }

        //CRUD HTTP GET operations
        [HttpGet("create")]
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet("details")]
        public IActionResult Details(int id)
        {
            var oneUserDB = new UserGetOneDB("DataSource=Data\\app.db", id);
            return View(oneUserDB.getUser());
        }
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            var oneUserDB = new UserGetOneDB("DataSource=Data\\app.db", id);
            return View(oneUserDB.getUserVM());
        }
        [HttpGet("changePassword")]
        public IActionResult ChangePassword(int id)
        {
            var oneUserDB = new UserGetOneDB("DataSource=Data\\app.db", id);
            return View(oneUserDB.getUserVM());
        }
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            var oneUserDB = new UserGetOneDB("DataSource=Data\\app.db", id);
            return View(oneUserDB.getUserVM());
        }


        //CRUD HTTP POST operations
        [HttpPost("create")]
        public IActionResult ProcessCreate(AppUserVM user)
        {
            UserAddEditDeleteDB adderService = new UserAddEditDeleteDB(user);
            if (adderService.AddUser())
            {
                TempData["pass"] = "Account has been successfully Added. You can sign in.";
                UserGetAllDB allUsers = new UserGetAllDB("DataSource=Data\\app.db");
                List<AppUser> users = allUsers.getAllUsers();
                return View("list", users);
            }
            else
            {
                TempData["error"] = "some error occured please try again later";
                return View("list");
            }
        }
        [HttpPost("Edit")]
        public IActionResult ProcessEdit(AppUserVM user)
        {

            UserAddEditDeleteDB usertoedit = new UserAddEditDeleteDB(user);
            if (usertoedit.EditUser())
            {
                TempData["pass"] = "Account has been successfully modfied. You can sign in.";
                UserGetAllDB allUsers = new UserGetAllDB("DataSource=Data\\app.db");
                List<AppUser> users = allUsers.getAllUsers();
                return View("list", users);
            }
            else
            {
                TempData["error"] = "some error occured please try again later";
                return View("list");
            }

        }
        [HttpPost("ChangePassword")]
        public IActionResult ProcessChangePassword(AppUserVM user)
        {

            UserAddEditDeleteDB usertoedit = new UserAddEditDeleteDB(user);
            if (usertoedit.ChangeUserPassword())
            {
                TempData["pass"] = "Password has been successfuly changed";
                UserGetAllDB allUsers = new UserGetAllDB("DataSource=Data\\app.db");
                List<AppUser> users = allUsers.getAllUsers();
                return View("list", users);
            }
            else
            {
                TempData["error"] = "some error occured please try again later";
                return View("list");
            }

        }
        [HttpPost("delete")]
        public IActionResult ProcessDelete(AppUserVM user)
        {
            var oneUserDB = new UserGetOneDB("DataSource=Data\\app.db", user.UserId);
            user = oneUserDB.getUserVM();
            UserAddEditDeleteDB usertodelete = new UserAddEditDeleteDB(user);

            if (usertodelete.DeleteUser())
            {
                TempData["pass"] = "Account has been successfully removed";
                UserGetAllDB allUsers = new UserGetAllDB("DataSource=Data\\app.db");
                List<AppUser> users = allUsers.getAllUsers();
                return View("list", users);
            }
            else
            {
                TempData["error"] = "some error occured please try again later";
                return View("list");
            }

        }
     
        //Login GET & POST
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
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

        //Register GET & POST
        [HttpGet("register")]
        public IActionResult Register(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("register")]
        public IActionResult ProcessRegister(AppUserVM user)
        {

            UserAddEditDeleteDB usertoadd = new UserAddEditDeleteDB(user);
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

        //Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


        //DENIED Access
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }


        

    }
}
