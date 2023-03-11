using DataLibrary.Business_Logic.User;
using DataLibrary.Models;
using MVCWebApplication.Models.User;
using System;
using System.Collections.Generic;
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
                Password = user.Password,
                FatherName = user.FatherName,
                Degree = user.Degree,
                Experience = user.Experience,
                Address = user.Address,
                JoiningDate = user.DateOfJoining,
                Department = user.Department
            };
            return View(data);
        }
        // User Edit POST Profile Page
        [HttpPost]
        public ActionResult EditProfile(User user)
        {
            Debug.WriteLine(user.FatherName);
            User_Model data_user = new User_Model()
            {
                Id = (int)Session["uId"],
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                FatherName = user.FatherName,
                Degree = user.Degree,
                Experience = user.Experience,
                Address = user.Address,
                DateOfJoining = user.JoiningDate,
                Department = user.Department
            };
            User_Data_Process.EditUserProfile(data_user);
            return RedirectToAction("Show");
        }

        //Apply For Leave
        public ActionResult LeaveApply()
        {

            return View();
        }
        [HttpPost]
        public ActionResult LeaveApply(Leave leave)
        {
            string day1 = leave.StartingDate;
            string day2 = leave.EndingDate;
            DateTime dt1 = Convert.ToDateTime(day1);
            DateTime dt2 = Convert.ToDateTime(day2);
            TimeSpan span = dt2.Subtract(dt1);
            int totalDays = span.Days + 1;
            if (totalDays < 1)
            {
                ModelState.AddModelError("", "Add proper dates of leave");
                return View();
            }
            Debug.WriteLine(span.Days);
            int Id = (int)Session["uId"];
            User_Data_Process.UserApplyForLeave(Id, leave.Reason, leave.StartingDate, leave.EndingDate, totalDays);
            return View();
        }

        //Leave Current Status
        public ActionResult LeaveStatus()
        {
            int Id = (int)Session["uId"];
            List<LeaveDetails> leaveDetailsLst = User_Data_Process.ShowLeaveStatus(Id);
            List<LeaveStatus> data = new List<LeaveStatus>();
            foreach (LeaveDetails leave in leaveDetailsLst)
            {
                LeaveStatus status = new LeaveStatus()
                {
                    CasualLeaves = leave.Leave.CL,
                    PaidLeaves = leave.Leave.PL,
                    Reason = leave.Reason,
                    RejectionReason = leave.ReasonForDenial,
                    Status = leave.Status,
                    StartingDate = leave.Starting,
                    EndingDate = leave.Ending
                };
                data.Add(status);
            }
            return View(data);
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
                Email = data_user.Email,
                FatherName = data_user.FatherName,
                Degree = data_user.Degree,
                Experience = data_user.Experience,
                Address = data_user.Address,
                JoiningDate = data_user.DateOfJoining,
                Department = data_user.Department

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