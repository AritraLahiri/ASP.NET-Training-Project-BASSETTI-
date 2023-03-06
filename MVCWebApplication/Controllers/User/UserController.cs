using DataLibrary.Business_Logic.User;
using DataLibrary.Models;
using MVCWebApplication.Models.User;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCWebApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        // User Index Page
        public ActionResult Index()
        {
            return View();
        }
        // User Edit GET Profile Page
        public ActionResult EditProfile()
        {
            int Id = (int)Session["uId"];
            Debug.WriteLine(Id);
            User_Model user = User_Data_Process.GetUserProfileById(Id);
            User data = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };
            return View(data);
        }
        // User Edit POST Profile Page
        [HttpPost]
        public ActionResult EditProfile(User user)
        {
            User_Model data_user = new User_Model()
            {
                Id = (int)Session["uId"],
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password
            };
            User_Data_Process.EditUserProfile(data_user);
            return RedirectToAction("Show");
        }
        // User Show Profile Details Page
        public ActionResult Show()
        {
            int Id = (int)Session["uId"];
            User_Model data_user = User_Data_Process.GetUserProfileById(Id);
            User user = new User()
            {
                FirstName = data_user.FirstName,
                LastName = data_user.LastName,
                Email = data_user.Email

            };
            return View(user);
        }
        // User Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}