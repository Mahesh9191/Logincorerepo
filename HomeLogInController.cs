using CorePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CorePractice.Controllers
{
    public class HomeLogInController : Controller
    {
        private readonly LogInDbContext dbcontext;

        public HomeLogInController(LogInDbContext Dbcontext)
        {
            this.dbcontext = Dbcontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LogIn login)
        {
            var mylogins=dbcontext.LogIns.Where(x=>x.Email==login.Email && x.Password==login.Password).FirstOrDefault();
            
            if (mylogins != null)
            {
                HttpContext.Session.SetString("UserSession", login.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed";
            }
            return View();
        }

        public IActionResult Dashboard()
        {
           if( HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession=HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("LogIn");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LogIn login)
        {
            if (ModelState.IsValid)
            {
               await dbcontext.LogIns.AddAsync(login);
                await dbcontext.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully..";
                return RedirectToAction("LogIn");
            }
            return View();
        }
    }
}
