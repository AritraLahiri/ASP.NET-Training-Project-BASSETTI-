using DataLibrary.Business_Logic;
using MVCWebApplication.Models;
using System.Collections.Generic;
using System.Diagnostics;
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
                    employee.Password);
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
                Debug.WriteLine(employeeId);
                Debug.WriteLine(id);
                ProcessData.UpdateEmployee(id, employeeId, email, firstName, lastName);
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
                    EmployeeId = emp.EmployeeId
                });
            }
            return View(employeeList);
        }

        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }

}