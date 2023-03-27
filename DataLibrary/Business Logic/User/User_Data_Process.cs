using DataLibrary.DataAcess.User;
using DataLibrary.Models;
using System.Collections.Generic;

namespace DataLibrary.Business_Logic.User
{
    public static class User_Data_Process
    {

        public static void EditUserProfile(User_Model user)
        {
            UserDataAccess.UpdateDataInDb(user);
        }

        public static User_Model GetUserProfileById(int id)
        {
            return UserDataAccess.SearchUserById(id);
        }

        /*public static void CreateUserProfile(User_Model user)
        {
            UserDataAccess.CreateUserInDb(user);
        }*/

        public static int CheckUserLogin(string email, string password)
        {
            return UserDataAccess.LogInUser(email, password);
        }

        public static bool CheckIfUserIsAdmin(int Id)
        {
            return UserDataAccess.IsAdmin(Id);
        }

        public static void UserApplyForLeave(int empId, string reason, string start, string end, int totalDays)
        {
            UserDataAccess.ApplyForLeave(empId, reason, start, end, totalDays);
        }

        public static List<LeaveDetails> ShowLeaveStatus(int empId)
        {
            return UserDataAccess.ViewLeaves(empId);
        }

    }
}
