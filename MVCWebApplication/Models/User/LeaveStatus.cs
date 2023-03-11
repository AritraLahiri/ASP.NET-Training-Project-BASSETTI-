using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication.Models.User
{
    public class LeaveStatus
    {
        [Required]
        [Display(Name = " Reason ")]
        public string Reason { get; set; }

        [Required]
        [Display(Name = " From ")]
        [DataType(DataType.Date)]
        public string StartingDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = " To ")]
        public string EndingDate { get; set; }


        [Required]
        [Display(Name = " Status ")]
        public string Status { get; set; }

        [Required]
        [Display(Name = " Casual Leaves ")]
        public int CasualLeaves { get; set; }

        [Required]
        [Display(Name = " Paid Leaves ")]
        public int PaidLeaves { get; set; }

        [Required]
        [Display(Name = " Reason For Denial ")]
        public string RejectionReason { get; set; }



    }
}