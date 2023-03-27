using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataLibrary.DataAcess.User
{
    static class UserDataAccess
    {
        //EDIT USER PROFILE
        internal static void UpdateDataInDb(User_Model user)
        {
            using (SqlConnection con = new SqlConnection(SQLDataAccess.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("spUser_UpdateProfile", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                cmd.Parameters.AddWithValue("@lastName", user.LastName);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@degree", user.Degree);
                cmd.Parameters.AddWithValue("@experience", user.Experience);
                cmd.Parameters.AddWithValue("@fatherName", user.FatherName);
                cmd.Parameters.AddWithValue("@department", user.Department);
                cmd.Parameters.AddWithValue("@doj", user.DateOfJoining);
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

        // GET THE DETAILS OF A USER
        // SEARCH BY ID
        internal static User_Model SearchUserById(int id)
        {

            using (SqlConnection con = new SqlConnection(SQLDataAccess.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("spUser_GetById ", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader;
                User_Model user = new User_Model();
                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Id = (int)reader["Id"];
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.DateOfJoining = reader["JoiningDate"].ToString();
                        user.Address = reader["Address"].ToString();
                        user.FatherName = reader["FatherName"].ToString();
                        user.Degree = reader["Degree"].ToString();
                        user.Experience = (int)reader["Experience"];
                        user.Department = reader["Department"].ToString();
                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }
                return user;
            }

        }

        // CREATE A NEW USER 
        /*internal static void CreateUserInDb(User_Model user)
        {

            SqlConnection con = new SqlConnection(SQLDataAccess.GetConnectionString());
            SqlCommand cmd = new SqlCommand("spUser_Create", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@password", user.Password);
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

        }*/

        //LOGIN USER
        internal static int LogInUser(string email, string password)
        {

            using (SqlConnection con = new SqlConnection(SQLDataAccess.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("spUser_Login", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                int id = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = (int)reader["Id"];

                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }
                return id;

            }

        }

        //CHECK IF ADMIN
        internal static bool IsAdmin(int id)
        {
            using (SqlConnection con = new SqlConnection(SQLDataAccess.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("spLogin_CheckIfAdmin", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                bool isAdmin = false;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    isAdmin = reader.Read();
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }
                return isAdmin;

            }
        }

        //Apply FOR LEAVE AS USER
        internal static void ApplyForLeave(int EmpId, string Reason, string Start, string End, int TotalDays)
        {
            using (SqlConnection con = new SqlConnection(SQLDataAccess.GetConnectionString()))
            {
                // FETCHING DETAILS FROM LEAVE TABLE THROUGH ID
                SqlCommand cmd = new SqlCommand("spLeave_GetById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", EmpId);
                Leaves leave = new Leaves();
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        leave.Id = (int)reader["Id"];
                        leave.EmployeeId = (int)reader["EmpId"];
                        leave.CL = (int)reader["CL"];
                        leave.PL = (int)reader["PL"];
                    }
                    reader.Close();
                    // INSERT INTO LEAVE DETAILS TABLE
                    cmd = new SqlCommand("spLeaveDetails_AddLeave", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("leaveId", leave.Id);
                    cmd.Parameters.AddWithValue("reason", Reason);
                    cmd.Parameters.AddWithValue("start", Start);
                    cmd.Parameters.AddWithValue("end", End);
                    cmd.Parameters.AddWithValue("totalDays", TotalDays);
                    cmd.Parameters.AddWithValue("status", "Pending");
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("RECORD INSERTED Successfully");
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }
            }

        }

        //View My Leaves as User
        internal static List<LeaveDetails> ViewLeaves(int empId)
        {
            using (SqlConnection con = new SqlConnection(SQLDataAccess.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("spJOIN_ShowLeaves", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@empId", empId);
                List<LeaveDetails> leaveDetailsLst = new List<LeaveDetails>();
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LeaveDetails leaveDetails = new LeaveDetails();
                        leaveDetails.Leave = new Leaves()
                        {
                            Id = (int)reader["Id"],
                            EmployeeId = (int)reader["EmpId"],
                            CL = (int)reader["CL"],
                            PL = (int)reader["PL"],
                        };
                        leaveDetails.Reason = reader["Reason"].ToString();
                        leaveDetails.Status = reader["Status"].ToString();
                        leaveDetails.Starting = reader["StartingFrom"].ToString();
                        leaveDetails.Ending = reader["EndingAt"].ToString();
                        leaveDetails.TotalDays = (int)reader["TotalDays"];
                        leaveDetails.ReasonForDenial = reader["ReasonForDenial"].ToString();
                        leaveDetailsLst.Add(leaveDetails);
                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error Generated. Details: " + e.Message);
                }
                return leaveDetailsLst;
            }
        }



    }

}
