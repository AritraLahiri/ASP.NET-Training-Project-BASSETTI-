namespace DataLibrary.Models.Admin
{
    public class Admin_LeaveDetails
    {
        public int Id { get; set; }
        public int CL { get; set; }
        public int PL { get; set; }
        public int Days { get; set; }
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string StartingFrom { get; set; }
        public string EndingAt { get; set; }
        public string Reason { get; set; }

    }
}
