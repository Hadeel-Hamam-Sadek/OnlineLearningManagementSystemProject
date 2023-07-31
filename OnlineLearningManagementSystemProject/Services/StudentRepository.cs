using Microsoft.AspNetCore.Mvc;
using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public class StudentRepository : IStudentRepository
    {
        LMSContext context;

        public StudentRepository(LMSContext _context)
        {
            context = _context;
        }

        public int CreateStudent(IFormFile formFile, [Bind("FirstName", "LastName", "NationalID", "UniversityID", "Mobile", "Email", "EnrollYear")] Student std)
        {

            if (formFile != null && formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(std.formFile.FileName);
                string relativePath = Path.Combine("studentimage", fileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                std.StudentPicture = relativePath;
                context.Students.Add(std);
                int raw = context.SaveChanges();
                return raw;
            }
            else if (formFile == null || formFile.Length < 0)
            {
                string relativePath = "images/blank-profile-picture-973460_640-5-600x600.png";
                std.StudentPicture = relativePath;

                context.Students.Add(std);
                int raw = context.SaveChanges();
                return raw;
            }
            return -1;
        }

        public int deleteStudent(int id)
        {
            Student student = context.Students.FirstOrDefault(s => s.StudentID == id);
            context.Students.Remove(student);
            int raw = context.SaveChanges();
            return raw;
        }

        public Student GetStudentById(string id)
        {
            return context.Students.FirstOrDefault(s => s.UserID == id);
        }
        public Student GetStudentById(int id)
        {
            return context.Students.FirstOrDefault(s => s.StudentID == id);
        }



        public List<Student> GetStudents()
        {
            return context.Students.ToList();
        }

        public int updateStudent(int id, Student student)
        {
            Student oldstd = context.Students.FirstOrDefault(s => s.StudentID == id);
            oldstd.FirstName = student.FirstName;
            oldstd.LastName = student.LastName;
            oldstd.EnrollYear = student.EnrollYear;
            oldstd.Mobile = student.Mobile;
            oldstd.NationalID = student.NationalID;
            oldstd.UniversityID = student.UniversityID;
            int raw = context.SaveChanges();
            return raw;
        }
        public List<EnrollYearStudentNameViewModel> GetStudentsASEnrollYear()
        {
            var students = context.Students.ToList();
            var enrollYearStudentNameList = new List<EnrollYearStudentNameViewModel>();
            foreach (var student in students)
            {
                enrollYearStudentNameList.Add(new EnrollYearStudentNameViewModel
                {
                    studentid = student.StudentID,
                    studentname = student.FirstName + " " + student.LastName,
                    enrollyear = student.EnrollYear
                });
            }
            return enrollYearStudentNameList;

        }
    }
}
