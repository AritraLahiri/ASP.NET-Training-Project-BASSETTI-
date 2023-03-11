using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication.Models.Admin
{
    public class LeaveDetails
    {

        [Display(Name = "Total CL")]
        public int CL { get; set; }

        [Display(Name = "Total PL")]
        public int PL { get; set; }

        [Display(Name = "Number Of Days")]
        public int Days { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = " Employee Id ")]
        public int EmployeeId { get; set; }

        [Display(Name = "Name ")]
        public string EmployeeName { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Starting From")]
        public string StartingFrom { get; set; }

        [Display(Name = "Ending At")]
        public string EndingAt { get; set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }

        [Display(Name = " Accept Leave ")]
        public bool Accepted { get; set; }

        [Display(Name = " Reject Leave ")]
        public bool Rejected { get; set; }

        [Display(Name = " Reason ")]
        public string ReasonForReject { get; set; }



    }
}