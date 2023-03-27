using DataLibrary.DataAcess;
using DataLibrary.Models;
using DataLibrary.Models.Admin;
using System.Collections.Generic;

namespace DataLibrary.Business_Logic
{
    public static class ProcessData
    {

        public static void CreateEmployee(int empId, string email, string firstName, string lastName, string pass,
            string fatherName, string degree, int experience, string address, string doj, string department)
        {
            Employee employee = new Employee()
            {
                EmployeeId = empId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = pass,
                FatherName = fatherName,
                Degree = degree,
                Experience = experience,
                Address = address,
                DateOfJoining = doj,
                Department = department

            };
            SQLDataAccess.InsertDataInDb(employee);

        }

        public static List<Employee> ShowAllEmployees()
        {
            return SQLDataAccess.LoadData();
        }

        public static void DeleteEmployee(int empId)
        {

            SQLDataAccess.DeleteDataInDb(empId);


        }

        public static void UpdateEmployee(int Id, int empId, string email, string firstName, string lastName,
            string fatherName, string degree, int experience, string address, string doj, string department)
        {

            Employee employee = new Employee()
            {
                Id = Id,
                EmployeeId = empId,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                FatherName = fatherName,
                Degree = degree,
                Experience = experience,
                Address = address,
                DateOfJoining = doj,
                Department = department
            };
            SQLDataAccess.UpdateDataInDb(employee);
        }

        public static Employee SearchEmployeeById(int id)
        {
            return SQLDataAccess.SearchEmployeeById(id);
        }

        public static List<Employee> SearchByName(string name)
        {
            return SQLDataAccess.SearchByName(name);
        }


        public static List<LeaveRequests> ShowLeaveRequests()
        {
            return SQLDataAccess.GetLeaveRequest();
        }

        public static Admin_LeaveDetails GetLeaveDetailsById(int id)
        {
            return SQLDataAccess.GetLeaveRequestById(id);
        }


        public static void AcceptOrRejectLeave(int Id, bool isAccepted)
        {
            SQLDataAccess.GrantOrRejectLeave(Id, isAccepted);
        }




    }
}
