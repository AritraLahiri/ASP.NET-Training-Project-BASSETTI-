using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication.Models.Admin
{
    public class LeaveRequests
    {

        public int Id { get; set; }
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

    }
}