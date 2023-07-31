using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningManagementSystemProject.Models
{
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialID { get; set; }
        [Required(ErrorMessage = "Please Enter Your MaterialName"), MaxLength(50)]
        [Display(Name = "Title")]
        public string MaterialName { get; set; }
        [MaxLength(50)]
        public string MaterialDate { get; set; }
        public MaterialType MaterialType { get; set; }
        [Required(ErrorMessage = "Please Enter Your Link")]
        [Display(Name = "Link")]
        public string MaterialPath { get; set; }
        [NotMapped]
        [Display(Name = "Attach")]
        public IFormFile? formFile { get; set; }
        public int CourseID { get; set; } // Course 1 To M Mateial
        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
    }
    public enum MaterialType
    {
        Document,
        Video,
        Link
    }

}
