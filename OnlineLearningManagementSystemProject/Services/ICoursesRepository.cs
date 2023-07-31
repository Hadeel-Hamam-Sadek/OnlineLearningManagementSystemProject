using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public interface ICoursesRepository
    {
        List<Course> GetCourse();
        Course GetCourseById(int id);
        int CreateCourse(IFormFile formFile, Course crs);
        int deleteCourse(int id);
        int updateCourse(int id, Course crs);
        DoctorStudentViewModel DoctorStudentName(int courseid);
    }
}
