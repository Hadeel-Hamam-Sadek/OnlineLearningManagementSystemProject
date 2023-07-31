using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();
        Student GetStudentById(string id);
        Student GetStudentById(int id);
        int CreateStudent(IFormFile formFile, Student std);
        int deleteStudent(int studentid);
        int updateStudent(int id, Student std);
        List<EnrollYearStudentNameViewModel> GetStudentsASEnrollYear();
    }
}
