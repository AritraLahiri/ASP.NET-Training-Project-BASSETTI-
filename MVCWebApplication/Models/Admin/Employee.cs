using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication.Models
{
    public class Employee
    {

        [Range(100000, 999999, ErrorMessage = "Please enter valid Employee Id")]
        [Display(Name = "Employee Id")]
        [Required]
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

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
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessage = "Email Id donot match")]
        public string ConfirmEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Please provide a password more than 9 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password donot match")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}