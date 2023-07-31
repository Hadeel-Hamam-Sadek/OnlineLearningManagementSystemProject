using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningManagementSystemProject.Models
{
    public class Course
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Please Enter Your Course Name")]
        [Display(Name = " Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Course Code")]
        [Display(Name = " Course Code")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Please Enter Course’s Hours ")]
        [Display(Name = " Credit Hours")]
        public int Credit { get; set; }
        [Display(Name = " Lecture Hours")]
        public int Lecture { get; set; }
        [Display(Name = " Lab Hours")]
        public int Lab { get; set; }
        [Display(Name = " Training Hours")]
        public int Training { get; set; }
        [Display(Name = "Picture")]
        public string CourseImage { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<StaffCourse> staffcourses { get; set; }
        public ICollection<Material> Materials { get; set; }  // Course 1 To M Mateial
        //public ICollection<Assignment> Assignments { get; set; } // // Course 1 to M Assignment
    }
}
