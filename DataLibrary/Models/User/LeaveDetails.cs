namespace DataLibrary.Models
{
    public class LeaveDetails
    {
        public Leaves Leave { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string ReasonForDenial { get; set; }
        public int TotalDays { get; set; }
        public string Starting { get; set; }
        public string Ending { get; set; }
    }
}
