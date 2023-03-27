using DataLibrary.Business_Logic.User;
using DataLibrary.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Employee Mangement Portal";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Employee Portal Contact Page";

            return View();
        }

        //GET UserSignIn
        public ActionResult UserSignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserSignIn(string Email, string Password)
        {

            if (ModelState.IsValid)
            {
                int Id = User_Data_Process.CheckUserLogin(Email, Password);
                //Debug.WriteLine(Id > 0 ? " Login Successfull" : "Invalid Attempt");
                bool IsAdmin = User_Data_Process.CheckIfUserIsAdmin(Id);
                if (Id > 0)
                {
                    User_Model userData = User_Data_Process.GetUserProfileById(Id);
                    string userName = userData.FirstName;
                    FormsAuthentication.SetAuthCookie(userName, true);
                    Session["uId"] = Id;
                    if (IsAdmin) return RedirectToAction("Index", "Admin");
                    else return RedirectToAction("Index", "User");
                }
                ModelState.AddModelError("", "Invalid UserName Or Password");
            }
            return View();
        }

    }
}