using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication.Models.User
{
    public class Leave
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
    }
}