namespace DataLibrary.Models.Admin
{
    public class LeaveRequests
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Reason { get; set; }
        public string Department { get; set; }
        public string StartingFrom { get; set; }
        public string EndingAt { get; set; }

    }
}
