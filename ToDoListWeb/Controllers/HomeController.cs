using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoListWeb.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoListWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InterfaceClient I_Client;
        public HomeController(ILogger<HomeController> logger, InterfaceClient i_Client)
        {
            _logger = logger;
            I_Client = i_Client;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await I_Client.GetAllTasks();
            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            var users = await I_Client.GetAllUsers();
            List<int> ids = new List<int>();
            foreach (var user in users)
                ids.Add(user.UserId);
            ViewBag.Users = ids;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromForm] ToDoList.Models.TaskDTO task)
        {
            if (ModelState.IsValid)
            {
                var user = await I_Client.GetUser(task.TaskUserId);
                task.TaskUserId = user.UserId;
                await I_Client.CreateTask(task);
                return RedirectToAction("Index");
            }
            else return View("CreateTask");
        }

        [HttpGet]
        public async Task<IActionResult> RedactTaskView([FromQuery] string search, Tasks task)
        {
            var tasks = await I_Client.GetTask(Convert.ToInt32(search));

            var users = await I_Client.GetAllUsers();
            List<int> ids = new List<int>();
            foreach (var user in users)
                ids.Add(user.UserId);
            ViewBag.Users = ids;

            return View(tasks);
        }
        [HttpPost]
        public async Task<IActionResult> RedactTask([FromForm] ToDoList.Models.Tasks task)
        {
            if (ModelState.IsValid)
                await I_Client.RedactTask(task.TaskId, task);
            else return View("RedactTask");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask([FromForm] ToDoList.Models.Tasks task)
        {
            await I_Client.DeleteTask(task.TaskId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] ToDoList.Models.Users user)
        {
            if (ModelState.IsValid)
                await I_Client.CreateUser(user);
            else return View("CreateUser");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProfilesUsers()
        {
            var users = await I_Client.GetAllUsers();
            return View(users);
        }

        public async Task<IActionResult> LoginUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromForm] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var response = await I_Client.Authorize(user);
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                new (ClaimTypes.NameIdentifier, new JwtSecurityTokenHandler()
                .ReadJwtToken(response)
                .Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value),
                new ("AccessToken", response)
                }, CookieAuthenticationDefaults.AuthenticationScheme)),
                new AuthenticationProperties() { IsPersistent = true });
            }
            else return View("LoginUser");
            return Redirect("/");
        }

        public async Task<IActionResult> LogoutUser()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
