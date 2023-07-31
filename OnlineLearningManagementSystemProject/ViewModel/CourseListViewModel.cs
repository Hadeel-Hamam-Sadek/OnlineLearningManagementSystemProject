namespace OnlineLearningManagementSystemProject.ViewModel
{
    public class CourseListViewModel
    {
        public int StudentId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string nationalID { get; set; }
        public string universityID { get; set; }
        public string mobile { get; set; }
        public int enrollYear { get; set; }

        public List<CourseViewModel> StudentCourse { get; set; }
    }
}
