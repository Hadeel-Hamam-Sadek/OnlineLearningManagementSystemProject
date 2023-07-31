using System.ComponentModel.DataAnnotations;

namespace OnlineLearningManagementSystemProject.ViewModel
{
    public class LoginViewModel
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remmber Me")]

        public bool IsPersistent { get; set; }
    }
}
