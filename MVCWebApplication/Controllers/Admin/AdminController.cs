using DataLibrary.Business_Logic;
using MVCWebApplication.Models;
using MVCWebApplication.Models.Admin;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCWebApplication.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        //INDEX 
        public ActionResult Index()
        {
            ViewBag.Name = "Welcome Here Admin";
            return View();
        }

        //CREATE A EMPLOYEE IN DB
        public ActionResult SignUpEnd()
        {
            ViewBag.Message = "Employee SignUp Complete";
            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Employee SignUp Form";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Employee employee)
        {
            if (ModelState.IsValid)
            {
                ProcessData.CreateEmployee(employee.EmployeeId, employee.Email, employee.FirstName, employee.LastName,
                    employee.Password, employee.FatherName, employee.Degree, employee.Experience, employee.Address,
                    employee.JoiningDate, employee.Department);
                return RedirectToAction("ShowEmployees");
            }

            return View();
        }

        // GET: Employee
        public ActionResult ShowEmployees()
        {
            List<DataLibrary.Models.Employee> allEmployees = ProcessData.ShowAllEmployees();
            return View(allEmployees);
        }

        //EDIT ROUTE FOR EDITING EMPLOYEE
        public ActionResult EditEmployee(int Id)
        {
            DataLibrary.Models.Employee DataEmployee = ProcessData.SearchEmployeeById(Id);
            Employee employee = new Employee()
            {
                EmployeeId = DataEmployee.EmployeeId,
                FirstName = DataEmployee.FirstName,
                LastName = DataEmployee.LastName,
                Email = DataEmployee.Email,
                FatherName = DataEmployee.FatherName,
                Degree = DataEmployee.Degree,
                Experience = DataEmployee.Experience,
                Address = DataEmployee.Address,
                JoiningDate = DataEmployee.DateOfJoining,
                Department = DataEmployee.Department

            };
            return View(employee);
        }
        //EDIT ROUTE FOR EDITING EMPLOYEE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(int Id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                int id = Id, employeeId = employee.EmployeeId;
                string email = employee.Email,
                 firstName = employee.FirstName,
                 lastName = employee.LastName;
                ProcessData.UpdateEmployee(id, employeeId, email, firstName, lastName,
                employee.FatherName, employee.Degree, employee.Experience, employee.Address,
                employee.JoiningDate, employee.Department);
            }
            return RedirectToAction("ShowEmployees");

        }

        // DELETE ROUTE
        public ActionResult DeleteEmployee(int Id)
        {
            DataLibrary.Models.Employee employee = ProcessData.SearchEmployeeById(Id);
            return View(employee);
        }
        // DELETE ROUTE
        [HttpPost]
        public ActionResult DeleteEmployee(int id, Employee employee)
        {
            ProcessData.DeleteEmployee(id);
            return RedirectToAction("ShowEmployees");
        }

        //Searching
        public ActionResult SearchEmployee()
        {
            List<Employee> lst = new List<Employee>();
            return View(lst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchEmployee(string name)
        {
            List<DataLibrary.Models.Employee> dataEmployee = ProcessData.SearchByName(name);
            List<Employee> employeeList = new List<Employee>();
            foreach (var emp in dataEmployee)
            {
                employeeList.Add(new Employee()
                {
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Email = emp.Email,
                    EmployeeId = emp.EmployeeId,
                    FatherName = emp.FatherName,
                    Degree = emp.Degree,
                    Experience = emp.Experience,
                    Address = emp.Address,
                    JoiningDate = emp.DateOfJoining,
                    Department = emp.Department
                });
            }
            return View(employeeList);
        }

        // VIEW LEAVE REQUESTS MADE BY EMPLOYEE
        public ActionResult LeaveRequests()
        {
            List<DataLibrary.Models.Admin.LeaveRequests> data = ProcessData.ShowLeaveRequests();
            List<LeaveRequests> requests = new List<LeaveRequests>();
            foreach (var leaveRequest in data)
            {
                requests.Add(new LeaveRequests()
                {
                    Id = leaveRequest.Id,
                    EmployeeId = leaveRequest.EmployeeId,
                    EmployeeName = leaveRequest.EmployeeName,
                    Department = leaveRequest.Department,
                    EndingAt = leaveRequest.EndingAt,
                    StartingFrom = leaveRequest.StartingFrom,
                    Reason = leaveRequest.Reason


                });
            }
            return View(requests);
        }

        // GRANT OR REJECT LEAVE BY ADMIN
        public ActionResult GrantOrRejectLeave(int id)
        {
            DataLibrary.Models.Admin.Admin_LeaveDetails admin_LeaveDetails = ProcessData.GetLeaveDetailsById(id);
            LeaveDetails leaveDetails = new LeaveDetails()
            {
                CL = admin_LeaveDetails.CL,
                Days = admin_LeaveDetails.Days,
                Department = admin_LeaveDetails.Department,
                Email = admin_LeaveDetails.Email,
                EmployeeId = admin_LeaveDetails.EmployeeId,
                EmployeeName = admin_LeaveDetails.EmployeeName,
                EndingAt = admin_LeaveDetails.EndingAt,
                Reason = admin_LeaveDetails.Reason,
                StartingFrom = admin_LeaveDetails.StartingFrom,
                PL = admin_LeaveDetails.PL

            };
            return View(leaveDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GrantOrRejectLeave(int id, LeaveDetails modal)
        {
            if (ModelState.IsValid)
            {
                bool isAccepted = modal.Accepted;
                bool isRejected = modal.Rejected;
                ProcessData.AcceptOrRejectLeave(id, isAccepted && isAccepted);
                return RedirectToAction("LeaveRequests", "Admin");
            }
            ModelState.AddModelError("", "Please enter all the field to proceed");
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }

}