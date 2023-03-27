
using DataLibrary.Models;
using DataLibrary.Models.Admin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataLibrary.DataAcess
{
    static class SQLDataAccess
    {

        internal static string GetConnectionString(string connectionName = "MVCDataBase")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        // View Data
        internal static List<Employee> LoadData()
        {

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {

                SqlCommand cmd = new SqlCommand("spEmployee_GetAll", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                List<Employee> data = new List<Employee>();
                SqlDataReader reader;
                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        data.Add(new Employee()
                        {
                            Id = (int)reader["Id"],
                            EmployeeId = (int)reader["EmployeeId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                        });

                    }

                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }

                return data;
            }
        }

        // INSERT DATA IN DB
        internal static void InsertDataInDb(Employee employee)

        {

            SqlConnection con = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand("insertData", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@empId", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@email", employee.Email);
            cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@lastName", employee.LastName);
            cmd.Parameters.AddWithValue("@password", employee.Password);
            cmd.Parameters.AddWithValue("@address", employee.Address);
            cmd.Parameters.AddWithValue("@degree", employee.Degree);
            cmd.Parameters.AddWithValue("@experience", employee.Experience);
            cmd.Parameters.AddWithValue("@fatherName", employee.FatherName);
            cmd.Parameters.AddWithValue("@department", employee.Department);
            cmd.Parameters.AddWithValue("@doj", employee.DateOfJoining);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("RECORD INSERTED Successfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con?.Close();
            }
        }

        // UPDATA DATA IN DB
        internal static void UpdateDataInDb(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("updateData", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@empId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@email", employee.Email);
                cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                cmd.Parameters.AddWithValue("@address", employee.Address);
                cmd.Parameters.AddWithValue("@degree", employee.Degree);
                cmd.Parameters.AddWithValue("@experience", employee.Experience);
                cmd.Parameters.AddWithValue("@fatherName", employee.FatherName);
                cmd.Parameters.AddWithValue("@department", employee.Department);
                cmd.Parameters.AddWithValue("@doj", employee.DateOfJoining);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Record Updated Successfully");

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }

            }
        }


        // DELETE DATA TABLE IN DB
        internal static void DeleteDataInDb(int id)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("deleteData", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Debug.WriteLine("Deleted Table Successfully");
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }

        // SEARCH BY ID
        internal static Employee SearchEmployeeById(int id)
        {

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("searchData", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader;
                Employee employee = new Employee();
                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employee.Id = (int)reader["Id"];
                        employee.EmployeeId = (int)reader["EmployeeId"];
                        employee.Email = reader["Email"].ToString();
                        employee.FirstName = reader["FirstName"].ToString();
                        employee.LastName = reader["LastName"].ToString();
                        employee.DateOfJoining = reader["JoiningDate"].ToString();
                        employee.Address = reader["Address"].ToString();
                        employee.FatherName = reader["FatherName"].ToString();
                        employee.Degree = reader["Degree"].ToString();
                        employee.Experience = (int)reader["Experience"];
                        employee.Department = reader["Department"].ToString();
                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }
                return employee;
            }

        }

        // SEARCH BY Name Of Employee
        internal static List<Employee> SearchByName(string name)
        {

            List<Employee> data = new List<Employee>();
            try
            {
                string con = GetConnectionString();
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SearchByName", con);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Name", name);
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    data.Add(new Employee()
                    {
                        Id = (int)row["Id"],
                        Email = (string)row["Email"],
                        FirstName = (string)row["FirstName"],
                        LastName = (string)row["LastName"],
                        EmployeeId = (int)row["EmployeeId"]

                    });
                }


            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }

            return data;
        }


        // SEARCH FOR LEAVE REQUESTS MADE BY PARTICULAR USER/EMPLOYEE
        internal static List<LeaveRequests> GetLeaveRequest()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {

                SqlCommand cmd = new SqlCommand("spAdmin_ShowLeaveRequests", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                List<LeaveRequests> data = new List<LeaveRequests>();
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        data.Add(
                             new LeaveRequests()
                             {
                                 Id = (int)reader["Id"],
                                 EmployeeId = (int)reader["EmployeeId"],
                                 EmployeeName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString(),
                                 Department = reader["Department"].ToString(),
                                 Reason = reader["Reason"].ToString(),
                                 StartingFrom = reader["StartingFrom"].ToString(),
                                 EndingAt = reader["EndingAt"].ToString()
                             });

                    }

                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }

                return data;
            }


        }

        // SEARCH A LEAVE REQUEST BASED ON AN ID
        internal static Admin_LeaveDetails GetLeaveRequestById(int Id)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("spAdmin_GetLeaveById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader reader;
                Admin_LeaveDetails leaveDetails = new Admin_LeaveDetails();
                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        leaveDetails.Id = (int)reader["EmpId"];
                        leaveDetails.EmployeeId = (int)reader["EmployeeId"];
                        leaveDetails.EmployeeName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                        leaveDetails.Department = reader["Department"].ToString();
                        leaveDetails.Reason = reader["Reason"].ToString();
                        leaveDetails.StartingFrom = reader["StartingFrom"].ToString();
                        leaveDetails.EndingAt = reader["EndingAt"].ToString();
                        leaveDetails.Email = reader["Email"].ToString();
                        leaveDetails.CL = (int)reader["CL"];
                        leaveDetails.PL = (int)reader["PL"];
                        leaveDetails.Days = (int)reader["TotalDays"];
                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }
                return leaveDetails;
            }
        }

        //GRANT/REJECT A PARTICULAR LEAVE REQUEST
        //DIFFICULT LOGIC
        internal static void GrantOrRejectLeave(int Id, bool isAccepted, string reasonForDeny = "N/A")
        {
            Admin_LeaveDetails details = GetLeaveRequestById(Id);
            string status = "Rejected";
            if (isAccepted)
            {
                if (details.Days > details.PL)
                {
                    reasonForDeny = "Not enough leaves available";
                    // NO LEAVE GRANTED
                }
                else if (details.Days > 3)
                {
                    details.PL -= details.Days;
                    status = "Granted";
                    // DEDUCT FROM PL

                }
                else if (details.CL > details.Days)
                {
                    details.CL -= details.Days;
                    status = "Granted";
                    // DEDUCT FROM CL
                }
            }

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("spAdmin_Leave_Decision", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@leave_Details_Id", Id);
                cmd.Parameters.AddWithValue("@CL", details.CL);
                cmd.Parameters.AddWithValue("@PL", details.PL);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@reason_for_deny", reasonForDeny);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("RECORD INSERTED Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }

            }
        }


    }
}
