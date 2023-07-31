using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        LMSContext context;

        public StudentCourseRepository(LMSContext _context)
        {
            context = _context;
        }
        public List<CourseListViewModel> GetAllStusent()
        {
            var query = (from studentCourse in context.StudentCourses
                         join student in context.Students on studentCourse.StudentID equals student.StudentID
                         join course in context.Courses on studentCourse.CourseID equals course.CourseId
                         group new { studentCourse, course } by student.StudentID into stg
                         select new CourseListViewModel
                         {
                             StudentId = stg.Key,
                             name = stg.Select(x => x.studentCourse.Student.FirstName + " " + x.studentCourse.Student.LastName).FirstOrDefault(),
                             nationalID = stg.Select(x => x.studentCourse.Student.NationalID).FirstOrDefault(),
                             universityID = stg.Select(x => x.studentCourse.Student.UniversityID).FirstOrDefault(),
                             mobile = stg.Select(x => x.studentCourse.Student.Mobile).FirstOrDefault(),
                             enrollYear = stg.Select(x => x.studentCourse.Student.EnrollYear).FirstOrDefault(),
                             StudentCourse = stg.Select(x => new CourseViewModel
                             {
                                 courseName = x.course.CourseName,
                                 courseCode = x.course.CourseCode
                             }).ToList()
                         });

            return query.ToList();
        }

        public List<CourseViewModel> GetCoursesStudent(string userid)
        {
            var std = context.Students.FirstOrDefault(s => s.UserID == userid);
            var availableCourses = context.Courses.Where(c => c.StudentCourses.Any(sc => sc.Student.UserID == userid));

            var vmdList = availableCourses.Select(c => new CourseViewModel
            {
                pathimage = c.CourseImage,
                courseid = c.CourseId,
                courseCode = c.CourseCode,
                courseName = c.CourseName,
            }).ToList();
            return vmdList;
        }

        public List<StudentCourseViewModel1> AddCoursesToStudent(int studentId)
        {
            var std = context.Students.FirstOrDefault(s => s.StudentID == studentId);
            var availableCourses = context.Courses.Where(c => !c.StudentCourses.Any(sc => sc.StudentID == studentId));

            var vmdList = availableCourses.Select(c => new StudentCourseViewModel1
            {
                StudentId = studentId,
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseCode = c.CourseCode
            }).ToList();

            return vmdList;
        }
        public int AddCoursesToStudent(int studentId, int courseId)
        {
            var std = context.Students.FirstOrDefault(s => s.StudentID == studentId);
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);

            if (std != null && course != null)
            {
                var stdcourse = new Models.StudentCourse
                {
                    StudentID = std.StudentID,
                    CourseID = course.CourseId,
                    Student = std,
                    Course = course
                };

                context.StudentCourses.Add(stdcourse);
                int raw = context.SaveChanges();
                return raw;
            }
            return 0;
        }
        public List<StudentCourseViewModel1> GetCoursesStudentByAdmin(int studentID)
        {
            var std = context.Students.FirstOrDefault(s => s.StudentID == studentID);
            var availableCourses = context.Courses.Where(c => c.StudentCourses.Any(sc => sc.Student.StudentID == studentID));

            var vmdList = availableCourses.Select(c => new StudentCourseViewModel1
            {
                StudentId = studentID,
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseCode = c.CourseCode
            }).ToList();

            return vmdList;
        }
        public int GetCoursesStudentByAdmin(int studentId, int courseId)
        {
            var studentcourse = context.StudentCourses.
                 FirstOrDefault(sc => sc.StudentID == studentId && sc.CourseID == courseId);
            if (studentcourse != null)
            {
                context.StudentCourses.Remove(studentcourse);
                return context.SaveChanges();
            }
            return 0;
        }
        public List<Student> GetStudentsByCourseId(int courseId)
        {
            var student = (from sc in context.StudentCourses
                           where sc.CourseID == courseId
                           select sc.Student).ToList();
            return student;
        }
    }
}
