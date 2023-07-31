using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public interface IStudentCourseRepository
    {
        List<CourseListViewModel> GetAllStusent();
        List<CourseViewModel> GetCoursesStudent(string studentId);
        List<StudentCourseViewModel1> AddCoursesToStudent(int studentId);
        int AddCoursesToStudent(int studentId, int courseId);
        List<StudentCourseViewModel1> GetCoursesStudentByAdmin(int StudentId);
        int GetCoursesStudentByAdmin(int studentId, int courseId);

        List<Student> GetStudentsByCourseId(int courseId);
    }
}
