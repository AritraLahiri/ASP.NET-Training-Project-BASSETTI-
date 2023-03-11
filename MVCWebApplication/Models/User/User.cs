using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication.Models.User
{
    public class User
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Required]
        [Display(Name = "Experience (in months)")]
        public int Experience { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        public string JoiningDate { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Please provide a password more than 9 characters")]
        public string Password { get; set; }
    }
}