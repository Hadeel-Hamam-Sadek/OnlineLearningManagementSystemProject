using System.ComponentModel.DataAnnotations;

namespace OnlineLearningManagementSystemProject.ViewModel
{
    public class StaffRegistrationViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password is not matched with ConfirmPassword"), DataType(DataType.Password)]// check if password == ConfirmPassword or not
        public string ConfirmPassword { get; set; }
    }
}
