//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace OnlineLearningManagementSystemProject.Models
//{
//    public class StudentFile
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int StudentFileID { get; set; }
//        [Required(ErrorMessage = "Please Enter Your StudentFileName"), MaxLength(50)]
//        public string StudentFileName { get; set; }
//        [Required]
//        public string StudentFileType { get; set; }
//        [MaxLength(150), Required(ErrorMessage = "Please Enter Your StudentFilePath")]
//        public string StudentFilePath { get; set; }
//        public int StudentID { get; set; } // Student 1 to M StudentFiles
//        [ForeignKey("StudentID")]
//        public Student Student { get; set; } // Student 1 to M StudentFiles
//        public Assignment Assignment { get; set; } // Assignment 1 to 1 StudentFile 
//    }
//}
