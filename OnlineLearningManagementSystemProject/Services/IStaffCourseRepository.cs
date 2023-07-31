using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public interface IStaffCourseRepository
    {
        List<StaffCourseViewModel> AddCoursesToStaff(int stdid);
        int AddCoursesToStaff(int stdid, int courseId);
        List<CourseViewModel> GetCoursesStaff(string staffUserId);
        List<StaffCourseViewModel> GetCoursesStaffByAdmin(int staffid);
        int GetCoursesStaffByAdmin(int staffid, int courseId);
        List<Staff> GetStaffsByCourseId(int courseId);
    }
}