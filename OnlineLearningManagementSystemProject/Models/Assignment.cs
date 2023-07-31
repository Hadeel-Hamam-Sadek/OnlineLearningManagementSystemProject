//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace OnlineLearningManagementSystemProject.Models
//{
//    public class Assignment
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [ForeignKey("StudentFile")]
//        public int AssignmentID { get; set; }
//        [Required(ErrorMessage = "Please Enter Your AssignmentName"), MaxLength(50)]
//        public string AssignmentName { get; set; }
//        public string AssignmentType { get; set; }
//        [MaxLength(150), Required(ErrorMessage = "Please Enter Your AssignmentPath")]
//        public string AssignmentPath { get; set; }

//        public int StaffID { get; set; } // Staff 1 to M Assignment
//        [ForeignKey("StaffID")]
//        public Staff Staff { get; set; } // Staff 1 to M Assignment

//        public int CourseID { get; set; } // Course 1 to M Assignment
//        [ForeignKey("CourseID")]
//        public Course Course { get; set; } // Course 1 to M Assignment

//        public StudentFile StudentFile { get; set; } // Assignment 1 to 1 StudentFile 
//    }
//}
